using Publink.Rest.Helpers;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _postsRepository;
		private readonly ILogger _logger;

		public PostService(IPostRepository postsRepository, ILogger<PostService> logger)
		{
			_postsRepository = postsRepository;
			_logger = logger;
		}

		public async Task<Post> Create(PostDto post)
		{
			_logger.LogDebug("Calling create on post repository with title {Title}", post.Title);

			var res = await _postsRepository.Create(post);

			return res;
		}

		public async Task<IList<Post>> GetAllRandom()
		{
			_logger.LogDebug("Getting all random posts");

			var posts = await _postsRepository.GetAll();

			if (!posts.Any())
			{
				return new List<Post>();
			}

			var lastPost = posts.OrderByDescending(x => x.Id).ToList().First();

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

		public async Task<Post?> GetById(int id)
		{
			_logger.LogDebug("Getting post with id {Id}", id);

			var post = await _postsRepository.GetById(id);

			return post ?? null;
		}

		public async Task<IList<Post>> GetByIdAndRandom(int id, int randomLength)
		{

			_logger.LogDebug("Getting post with id {Id}, and {AmountOfPosts} random posts", id, randomLength);

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
	}
}
