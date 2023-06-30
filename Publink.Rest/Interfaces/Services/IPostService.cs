using Publink.Rest.Models;
using Publink.Rest.Models.Dto;
using Publink.Rest.Models.Responses;

namespace Publink.Rest.Interfaces.Services
{
	public interface IPostService
	{
		Task<IList<Post>> GetAllRandom();
		Task<Post> Create(PostDto post, Guid userId);
		Task<IList<Post>> GetByIdAndRandom(Guid id, int randomLength);
		Task<IList<Post>> GetAllPostsByUserId(Guid userId);
		Task<IList<PostAndUserResponse>> GetPostAndUserByIdAndRandom(Guid id, int randomLength);
	}
}
