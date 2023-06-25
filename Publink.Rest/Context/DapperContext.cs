using MySqlConnector;
using System.Data;

namespace Publink.Rest.Context
{
	public class DapperContext
	{
		private readonly string? _connectionString;
		private readonly ILogger _logger;

		public DapperContext(IConfiguration configuration, ILogger<DapperContext> logger)
		{
			_connectionString = configuration.GetConnectionString("DatabaseConnection");
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
				_logger.LogError("Error while connecting to database: Message => {Message} - Stack Trace => {StackTrace}", ex.Message, ex.StackTrace);
				throw;
			}
		}
	}
}
