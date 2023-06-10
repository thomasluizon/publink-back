using Publink.Rest.Models;

namespace Publink.Rest.Interfaces
{
	public interface IPostService
	{
		Task<IEnumerable<Post>> GetAllRandomPosts();
	}
}
