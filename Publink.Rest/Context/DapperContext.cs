using MySqlConnector;
using System.Data;

namespace Publink.Rest.Context
{
	public class DapperContext
	{
		private readonly IConfiguration _configuration;
		private readonly string? _connectionString;
		private readonly ILogger _logger;

		public DapperContext(IConfiguration configuration, ILogger logger)
		{
			_configuration = configuration;
			_connectionString = _configuration.GetConnectionString("DatabaseConnection");
			_logger = logger;
		}

		public IDbConnection CreateConnection()
		{
			try
			{
				_logger.LogInformation("Connecting to database");
			 
				return new MySqlConnection(_connectionString);
			} 
			catch (Exception ex)
			{
				_logger.LogError($"Error while connecting to database: Message => {ex.Message} - Stack Trace => {ex.StackTrace}");
				throw;
			}
		}
	}
}
