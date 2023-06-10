using Publink.Rest.Interfaces;
using Publink.Rest.Models;

namespace Publink.Rest.Services
{
	public class PostRepository : IPostRepository
	{
		public Task<IEnumerable<Post>> GetAllRandomPosts()
		{
			var list = new List<Post>
			{
				new Post
				{
					Id = 1,
					Description = "Teste",
					ImgUrl = "Teste",
					Title = "Titulo teste"
				}
			};

			return Task.FromResult(list as IEnumerable<Post>);
		}
	}
}
