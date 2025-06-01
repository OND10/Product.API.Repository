namespace ProductAPI.VSA.Services
{
    public interface IJobService
    {
        public void FireAndForgetJob();
        public void RecuringJob();
        public void DelayedJob();
        public void ContinuationJob();

    }
}
