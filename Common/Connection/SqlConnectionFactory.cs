using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductAPI.VSA.Common.Connection
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string? _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnectionString");
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
