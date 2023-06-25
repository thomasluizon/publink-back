using System.Globalization;
using Dapper;
using Publink.Rest.Context;
using Publink.Rest.Enums;
using Publink.Rest.Extensions;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto.Users;

namespace Publink.Rest.Repository
{
	public class AuthRepository : IAuthRepository
	{
		private readonly ILogger _logger;
		private readonly string _baseLogErrorMessage;
		private readonly DapperContext _dbContext;

		public AuthRepository(ILogger<AuthRepository> logger, DapperContext dbContext)
		{
			_logger = logger;
			_baseLogErrorMessage = "Message => {0} - Stack Trace => {1}";
			_dbContext = dbContext;
		}

		public async Task<User?> GetUser(string email, string password)
		{
			try
			{
				_logger.LogInformation("Getting user with email {Email} from database", email);

				const string query = @"SELECT *
												 FROM User AS u
											   WHERE u.Email = @Email";

				using var connection = _dbContext.CreateConnection();

				var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });

				if (user == null)
				{
					return null;
				}

				var saltedAndHashedPassword = password.WithSalt(user.Salt).ToHash();

				return saltedAndHashedPassword != user.PasswordHash ? null : user;
			}
			catch (Exception ex)
			{
				_logger.LogError("Error while getting user from database: {Message}", string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}

		public async Task<Tuple<bool, ErrorTypes?>> Register(UserRegisterDto userRegisterDto)
		{
			try
			{
				var salt = DateTime.Now.ToString(CultureInfo.InvariantCulture);

				var user = new User
				{
					Id = Guid.NewGuid(),
					Email = userRegisterDto.Email,
					Username = userRegisterDto.Username,
					PasswordHash = userRegisterDto.Password.WithSalt(salt).ToHash(),
					Role = Roles.User,
					Salt = salt
				};

				const string query = @"INSERT INTO User (
											            Id,
											            Email,
											            Username,
											            Password_Hash,
											            Role,
											            Salt
											        ) VALUES (
															@Id,
															@Email,
															@Username,
															@PasswordHash,
															@Role,
															@Salt
										             );
											   SELECT LAST_INSERT_ID();";

				using var connection = _dbContext.CreateConnection();

				var insertedId = await connection.ExecuteScalarAsync<string>(query, new
				{
					user.Id,
					user.Email,
					user.Username,
					user.PasswordHash,
					Role = user.Role.ToString(),
					user.Salt
				});

				return new Tuple<bool, ErrorTypes?>(true, null);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("duplicate entry", StringComparison.CurrentCultureIgnoreCase))
				{
					_logger.LogError("Email already registered");
					return new Tuple<bool, ErrorTypes?>(false, ErrorTypes.DuplicateEntry);
				}

				_logger.LogError("Error while registering user on the database: {Message}", string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}
	}
}
