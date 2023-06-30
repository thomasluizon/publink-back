using Publink.Rest.Enums;
using Publink.Rest.Models.Dto.Users;
using Publink.Rest.Models.Responses;

namespace Publink.Rest.Interfaces.Services
{
    public interface IAuthService
	{
		Task<Tuple<string, UserResponse>?> Login(UserLoginDto userLoginDto);
		Task<Tuple<bool, ErrorTypes?>> Register(UserRegisterDto userRegisterDto);
	}
}
