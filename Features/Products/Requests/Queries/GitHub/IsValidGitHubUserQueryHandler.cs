using OnMapper.Common.Exceptions;
using ProductAPI.Abstractions.Messaging;
using System.Net;
using System.Text.Json.Nodes;

namespace ProductAPI.VSA.Features.Products.Requests.Queries.GitHub
{
    public class IsValidGitHubUserQueryHandler : IQueryHandler<IsValidGitHubUserQuery, bool>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IsValidGitHubUserQueryHandler(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<Result<bool>> Handle(IsValidGitHubUserQuery request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("GiHub");
            var response = await client.GetAsync($"/users/{request.username}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<JsonObject>();
                var message = responseBody!["message"]!.ToString();
                throw new HttpRequestException(message);
            }

            var result = response.StatusCode == HttpStatusCode.OK;
            return await Result<bool>.SuccessAsync(result, "Get User GitHyb verified Successfully", true);

        }
    }
}
