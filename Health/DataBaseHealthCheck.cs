using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductAPI.VSA.Common.Connection;
using System.Data;

namespace ProductAPI.VSA.Health
{
    public class DataBaseHealthCheck : IHealthCheck
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public DataBaseHealthCheck(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = await _dbConnectionFactory.CreateConnectionAsync();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT 1";
                //await command.Exc();
                return HealthCheckResult.Healthy();

            }
            catch (Exception exception)
            {
                return HealthCheckResult.Unhealthy(exception: exception);
            }
        }
    }
}
