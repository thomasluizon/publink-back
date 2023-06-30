using Publink.Rest.Models;

namespace Publink.Rest.Interfaces.Services
{
	public interface IUserService
	{
		public Task<User> GetByIdAsync(Guid id);
	}
}
