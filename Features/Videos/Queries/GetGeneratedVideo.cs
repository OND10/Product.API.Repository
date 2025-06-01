using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.ExceptionHandler;

namespace ProductAPI.VSA.Features.Videos.Queries
{
    public class GetGeneratedVideoQuery : IQuery<Result<List<string>>>
    {
        public string Prompt { get; set; } = null!;
    }
}
