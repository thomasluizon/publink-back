using Publink.Rest.Helpers;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto;
using Publink.Rest.Models.Responses;

namespace Publink.Rest.Services
{
    public class PostService : IPostService
	{
		private readonly IPostRepository _postsRepository;
		private readonly ILogger _logger;
		private readonly IUserService _userService;

		public PostService(IPostRepository postsRepository,  IUserService userService, ILogger<PostService> logger)
		{
			_postsRepository = postsRepository;
			_userService = userService;
			_logger = logger;
		}

		public async Task<Post> Create(PostDto post, Guid userId)
		{
			_logger.LogInformation("Calling create on post repository with title {Title}", post.Title);

			var res = await _postsRepository.Create(post, userId);

			return res;
		}

		public async Task<IList<Post>> GetAllRandom()
		{
			_logger.LogInformation("Getting all random posts");

			var posts = await _postsRepository.GetAll();

			if (!posts.Any())
			{
				return new List<Post>();
			}

			var lastPost = posts.OrderByDescending(x => x.CreateDate).ToList().First();

			var randomPosts = new List<Post>
			{
				lastPost
			};

			posts.Shuffle();

			var listToAdd = posts.ToList();

			listToAdd.Remove(lastPost);

			randomPosts.AddRange(listToAdd);

			return randomPosts;
		}

		public async Task<IList<Post>> GetByIdAndRandom(Guid id, int randomLength)
		{

			_logger.LogInformation("Getting post with id {Id}, and {AmountOfPosts} random posts", id, randomLength);

			var post = await GetById(id);

			if (post == null)
			{
				return new List<Post>();
			}

			var posts = new List<Post>
			{
				post
			};

			var randomPosts = await GetAllRandom();

			var postToRemove = randomPosts.First(x => x.Id == id);

			randomPosts.Remove(postToRemove);

			var i = 0;

			foreach (var p in randomPosts)
			{
				if (i == randomLength)
				{
					break;
				}

				posts.Add(p);
				i++;
			}

			return posts;
		}

		public async Task<IList<PostAndUserResponse>> GetPostAndUserByIdAndRandom(Guid id, int randomLength)
		{
			var posts = await GetByIdAndRandom(id, randomLength);

			var response = new List<PostAndUserResponse>();

			foreach (var post in posts)
			{
				var user = await _userService.GetByIdAsync(post.UserId);

				var postAndUserResponse = new PostAndUserResponse
				{
					User = user.ToUserResponse(),
					Post = post
				};

				response.Add(postAndUserResponse);
			}

			return response;
		}

		public async Task<Post?> GetById(Guid id)
		{
			_logger.LogInformation("Getting post with id {Id}", id);

			var post = await _postsRepository.GetById(id);

			return post ?? null;
		}

		public async Task<IList<Post>> GetAllPostsByUserId(Guid userId)
		{
			_logger.LogInformation("Getting all posts from user with id {UserId}", userId);

			var posts = await _postsRepository.GetAllPostsByUserId(userId);

			return posts;
		}
	}
}
