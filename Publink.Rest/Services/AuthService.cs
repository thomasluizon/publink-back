using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;
using Publink.Rest.Repository;

namespace Publink.Rest.Services
{
	public class AuthService : IAuthService
	{
		private readonly IAuthRepository _authRepository;
		private readonly ILogger _logger;

		public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger)
		{
			_authRepository = authRepository;
			_logger = logger;
		}

		public User? GetUser(string email, string password)
		{
			var user = _authRepository.GetUser(email, password);

			return user ?? null;
		}
	}
}
