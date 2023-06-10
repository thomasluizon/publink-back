using Publink.Rest.Models;

namespace Publink.Rest.Interfaces
{
	public interface IPostRepository
	{
		Task<IEnumerable<Post>> GetAllRandomPosts();
	}
}
