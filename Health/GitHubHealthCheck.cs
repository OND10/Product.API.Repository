using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductAPI.VSA.Features.Products.Requests.Queries.GitHub;

namespace ProductAPI.VSA.Health
{
    public class GitHubHealthCheck : IHealthCheck
    {
        private readonly ISender _sender;

        public GitHubHealthCheck(ISender sender)
        {
            _sender = sender;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var query = new IsValidGitHubUserQuery
            {
                username = "OND10"
            };

            try
            {
                await _sender.Send(query, cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception exception)
            {

                return HealthCheckResult.Unhealthy(exception: exception);
            }

        }

    }
}
