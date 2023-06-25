using Publink.Rest.Enums;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Models;

namespace Publink.Rest.Repository
{
	public class AuthRepository : IAuthRepository
	{
		private readonly ILogger _logger;

		public AuthRepository(ILogger<AuthRepository> logger)
		{
			_logger = logger;
		}

		public User? GetUser(string email, string password)
		{
			var users = new List<User>
			{
				new()
				{
					Email = "thomaslrgregorio@gmail.com",
					Password = "teste123",
					Id = Guid.NewGuid(),
					Username = "thomasluizon",
					Role = Roles.Admin
				},
				new()
				{
					Email = "thomasjeferson@hotmail.com",
					Password = "testeaaa",
					Id = Guid.NewGuid(),
					Username = "jeferson",
					Role = Roles.User
				}
			};

			var user = users
				.FirstOrDefault(user =>
					string.Equals(user.Email, email, StringComparison.CurrentCultureIgnoreCase)
					&& user.Password == password);

			return user ?? null;
		}
	}
}
