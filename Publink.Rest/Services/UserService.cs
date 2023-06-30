using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;

namespace Publink.Rest.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<User> GetByIdAsync(Guid id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			return user;
		}
	}
}
