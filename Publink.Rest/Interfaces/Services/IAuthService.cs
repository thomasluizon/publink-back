using Publink.Rest.Enums;
using Publink.Rest.Models.Dto.Users;

namespace Publink.Rest.Interfaces.Services
{
	public interface IAuthService
	{
		Task<Tuple<string, UserResponseDto>?> Login(UserLoginDto userLoginDto);
		Task<Tuple<bool, ErrorTypes?>> Register(UserRegisterDto userRegisterDto);
	}
}
