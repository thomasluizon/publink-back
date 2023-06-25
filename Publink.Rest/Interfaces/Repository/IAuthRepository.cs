using Publink.Rest.Enums;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto.Users;

namespace Publink.Rest.Interfaces.Repository
{
	public interface IAuthRepository
	{
		Task<User?> GetUser(string email, string password);
		Task<Tuple<bool, ErrorTypes?>> Register(UserRegisterDto userRegisterDto);
	}
}
