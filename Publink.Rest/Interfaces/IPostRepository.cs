using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Interfaces
{
	public interface IPostRepository
	{
		Task<IEnumerable<Post>> GetAllRandomPosts();
		Task<Post> AddPost(PostDto post);
	}
}
