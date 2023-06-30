using Publink.Rest.Models;

namespace Publink.Rest.Interfaces.Repository
{
	public interface IUserRepository
	{
		public Task<User> GetByIdAsync(Guid id);
	}
}
