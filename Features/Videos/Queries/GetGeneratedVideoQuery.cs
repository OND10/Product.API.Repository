using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.ExceptionHandler;

namespace ProductAPI.VSA.Features.Videos.Queries
{
    public class GetGeneratedVideoQuery : IQuery<List<string>>
    {
        public string Prompt { get; set; } = null!;
    }
}
