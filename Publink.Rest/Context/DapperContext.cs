using MySqlConnector;
using System.Data;

namespace Publink.Rest.Context
{
	public class DapperContext
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration.GetConnectionString("DatabaseConnection");
		}

		public IDbConnection CreateConnection()
			 => new MySqlConnection(_connectionString);
	}
}
