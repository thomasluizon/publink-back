using Dapper;
using Publink.Rest.Context;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Models;

namespace Publink.Rest.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly DapperContext _dbContext;
		private readonly ILogger _logger;
		private readonly string _baseLogErrorMessage;

		public UserRepository(ILogger<UserRepository> logger, DapperContext dbContext)
		{
			_dbContext = dbContext;
			_logger = logger;
			_baseLogErrorMessage = "Message => {0} - Stack Trace => {1}";
		}

		public async Task<User> GetByIdAsync(Guid id)
		{
			try
			{
				_logger.LogInformation("Getting user with id {Id} from database", id);

				const string query = @"SELECT *
												 FROM User u
												WHERE u.Id = @Id";

				using var connection = _dbContext.CreateConnection();

				var user = await connection.QuerySingleAsync<User>(query, new { Id = id });

				return user;
			}
			catch (Exception ex)
			{
				_logger.LogError("Error while getting user with id {Id} from database: {Message}", id, string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}
	}
}
