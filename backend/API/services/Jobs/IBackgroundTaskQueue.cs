namespace backend.services.jobs
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundItem(Func<CancellationToken, Task> item);
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken ct);
    }
}