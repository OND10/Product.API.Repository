using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.ExceptionHandler;
using ProductAPI.VSA.Features.Videos.Queries;

namespace ProductAPI.VSA.Features.Videos
{

    [ApiController]
    public class VideoController : ControllerBase
    {

        private readonly IQueryHandler<GetGeneratedVideoQuery, List<string>> _handle;
        public VideoController(IQueryHandler<GetGeneratedVideoQuery, List<string>> handle)
        {
            _handle = handle;
        }
        [Route("GenerateVideos")]
        [HttpPost]
        public async Task<Result<List<string>>> Post([FromForm] string prompt, CancellationToken cancellationToken)
        {
            var query = new GetGeneratedVideoQuery
            {
                Prompt = prompt
            };

            var result = await _handle.Handle(query, cancellationToken);

            return await Result<List<string>>.SuccessAsync(result.Data, "Get all video urls of the input prompt", true);
        }
    }
}
