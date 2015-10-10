using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMvvm
{
    // http://blogs.msdn.com/b/pfxteam/archive/2012/02/12/10266983.aspx
    public class AsyncSemaphore
    {
        private static readonly Task completed = Task.FromResult(true);
        private readonly Queue<TaskCompletionSource<bool>> waiters = new Queue<TaskCompletionSource<bool>>();
        private int currentCount;

        public AsyncSemaphore(int initialCount)
        {
            if (initialCount < 0)
            {
                throw new ArgumentOutOfRangeException("initialCount");
            }
            currentCount = initialCount;
        }

        public Task WaitAsync()
        {
            lock (waiters)
            {
                if (currentCount-- > 0)
                {
                    return completed;
                }
                var waiter = new TaskCompletionSource<bool>();
                waiters.Enqueue(waiter);
                return waiter.Task;
            }
        }

        public void Release()
        {
            lock (waiters)
            {
                if (waiters.Count <= 0)
                {
                    ++currentCount;
                    return;
                }
                var toRelease = waiters.Dequeue();
                toRelease?.SetResult(true);
            }
        }
    }
}
