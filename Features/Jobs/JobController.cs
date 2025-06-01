using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.VSA.Services;

namespace ProductAPI.VSA.Features.Jobs
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public JobController(IJobService jobService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }


        [HttpGet("/FireAndForgetJob")]
        public IActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());

            return Ok();
        }

        [HttpGet("/ScheduelJob")]
        public IActionResult CreateScheduledJob()
        {
            _backgroundJobClient.Schedule(() => _jobService.DelayedJob(), TimeSpan.FromMinutes(2));

            return Ok();
        }

        [HttpGet("/RecuringJob")]
        public IActionResult CreateRecuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobService.RecuringJob(), Cron.Minutely);

            return Ok();
        }

        [HttpGet("/ContiuationJob")]
        public IActionResult CreateContiuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobService.ContinuationJob());
            return Ok();
        }

    }
}
