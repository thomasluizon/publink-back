using Publink.Rest.Interfaces;
using Publink.Rest.Models;

namespace Publink.Rest.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _postsRepository;

		public PostService(IPostRepository postsRepository)
		{
			_postsRepository = postsRepository;
		}

		public async Task<IEnumerable<Post>> GetAllRandomPosts()
		{
			var posts = await _postsRepository.GetAllRandomPosts();

			return posts;
		}
	}
}
