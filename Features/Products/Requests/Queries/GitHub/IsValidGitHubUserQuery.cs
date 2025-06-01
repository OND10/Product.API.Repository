using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.ExceptionHandler;

namespace ProductAPI.VSA.Features.Products.Requests.Queries.GitHub
{
    public class IsValidGitHubUserQuery : IQuery<bool>
    {
        public string username { get; set; }
    }
}
