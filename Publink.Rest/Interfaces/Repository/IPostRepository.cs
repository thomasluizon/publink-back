using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Interfaces.Repository
{
	public interface IPostRepository
	{
		Task<IList<Post>> GetAll();
		Task<Post> Create(PostDto post, Guid userId);
		Task<Post?> GetById(Guid id);
		Task<IList<Post>> GetAllPostsByUserId(Guid userId);
	}
}
