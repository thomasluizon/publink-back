using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Interfaces
{
	public interface IPostRepository
	{
		Task<IList<Post>> GetAll();
		Task<Post> Create(PostDto post);
		Task<Post> GetById(int id);
	}
}
