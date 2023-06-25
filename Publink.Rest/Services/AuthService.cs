using Publink.Rest.Enums;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto.Users;

namespace Publink.Rest.Services
{
	public class AuthService : IAuthService
	{
		private readonly IAuthRepository _authRepository;
		private readonly ILogger _logger;
		private readonly ITokenService _tokenService;

		public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger, ITokenService tokenService)
		{
			_authRepository = authRepository;
			_tokenService = tokenService;
			_logger = logger;
		}

		public async Task<Tuple<string, UserResponseDto>?> Login(UserLoginDto userLoginDto)
		{
			var user = await GetUser(userLoginDto.Email, userLoginDto.Password);

			if (user == null)
			{
				return null;
			}

			var token = _tokenService.GenerateToken(user);

			return new Tuple<string, UserResponseDto>(token, new UserResponseDto
			{
				Username = user.Username,
				Id = user.Id
			});
		}

		public async Task<Tuple<bool, ErrorTypes?>> Register(UserRegisterDto userRegisterDto)
		{
			var isRegistered = await _authRepository.Register(userRegisterDto);

			return isRegistered;
		}

		private async Task<User?> GetUser(string email, string password)
		{
			var user = await _authRepository.GetUser(email, password);

			return user ?? null;
		}
	}
}
