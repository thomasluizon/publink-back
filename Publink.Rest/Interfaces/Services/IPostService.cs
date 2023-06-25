using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Interfaces.Services
{
	public interface IPostService
	{
		Task<IList<Post>> GetAllRandom();
		Task<Post> Create(PostDto post, Guid userId);
		Task<Post?> GetById(Guid id);
		Task<IList<Post>> GetByIdAndRandom(Guid id, int randomLength);
		Task<IList<Post>> GetAllPostsByUserId(Guid userId);
	}
}
