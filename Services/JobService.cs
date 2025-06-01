namespace ProductAPI.VSA.Services
{
    public class JobService : IJobService
    {
        private readonly ILogger<JobService> _logger;

        public JobService(ILogger<JobService> logger)
        {
            _logger = logger;
        }
        public void ContinuationJob()
        {
            _logger.LogInformation("Hello from a Continuation job!");
        }

        public void DelayedJob()
        {
            _logger.LogInformation("Hello from a Delayed job!");
        }

        public void FireAndForgetJob()
        {
            _logger.LogInformation("Hello from a FireAndForget job!");
        }

        public void RecuringJob()
        {
            _logger.LogInformation("Hello from a Recuring job!");
        }
    }
}
