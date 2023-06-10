using Publink.Rest.Interfaces;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _postsRepository;

		public PostService(IPostRepository postsRepository)
		{
			_postsRepository = postsRepository;
		}

		public async Task<Post> AddPost(PostDto post)
		{
			var res = await _postsRepository.AddPost(post);

			return res;
		}

		public async Task<IEnumerable<Post>> GetAllRandomPosts()
		{
			var posts = await _postsRepository.GetAllRandomPosts();

			return posts;
		}
	}
}
