
using System.Collections.Concurrent;

namespace backend.services.jobs
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> _functionQueue = new();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken ct)
        {
            await _signal.WaitAsync(ct);
            _functionQueue.TryDequeue(out var result);

            return result;
        }

        public void QueueBackgroundItem(Func<CancellationToken, Task> item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _functionQueue.Enqueue(item);
            _signal.Release();
        }
    }
}