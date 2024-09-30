
namespace backend.services.jobs 
{
    public class BackgroundJobService : BackgroundService
    {
        public IBackgroundTaskQueue _backgroundQueue {get;}
        public BackgroundJobService(IBackgroundTaskQueue backgroundQueue)
        {
            _backgroundQueue = backgroundQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while(ct.IsCancellationRequested == false)
            {
                var item = _backgroundQueue.DequeueAsync(ct);

                try
                {
                    await item;
                } catch {
                    throw;
                }
            }
        }
    }
}