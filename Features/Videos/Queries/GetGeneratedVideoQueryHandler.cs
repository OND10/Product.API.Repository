using Microsoft.Extensions.Options;
using OnMapper.Common.Exceptions;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.Settings;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
namespace ProductAPI.VSA.Features.Videos.Queries
{
    public class GetGeneratedVideoQueryHandler : IQueryHandler<GetGeneratedVideoQuery, List<string>>
    {

        private readonly PexelSettings _pexelSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public GetGeneratedVideoQueryHandler(IOptions<PexelSettings> pexelSettings, IHttpClientFactory httpClientFactory)
        {
            _pexelSettings = pexelSettings.Value;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<List<string>>> Handle(GetGeneratedVideoQuery query, CancellationToken cancellationToken)
        {
            List<string> listVideos = new List<string>();
            var pexelsClient = new PexelsClient(_pexelSettings.ApiKey);
            var result = await pexelsClient.SearchVideosAsync(query.Prompt);
            List<Video> lstvds = new List<Video>();
            lstvds = result.videos.ToList();

            foreach (var vid in lstvds)
            {
                //vid.user.id
                var vidoeLink = vid.videoFiles.FirstOrDefault().link;
                listVideos.Add(vidoeLink);
            }
            return await Result<List<string>>.SuccessAsync(listVideos, "Get all video urls of the input prompt", true);
        }
    }
}
