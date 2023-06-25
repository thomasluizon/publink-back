using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Interfaces.Services
{
    public interface IPostService
    {
        Task<IList<Post>> GetAllRandom();
        Task<Post> Create(PostDto post);
        Task<Post?> GetById(int id);
        Task<IList<Post>> GetByIdAndRandom(int id, int randomLength);
    }
}
