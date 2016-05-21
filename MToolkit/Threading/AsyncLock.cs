using System;
using System.Threading;
using System.Threading.Tasks;

namespace MToolkit.Threading
{
    /// <summary>
    ///     Class responsible for implementing async locks.
    /// </summary>
    /// <see cref="http://blogs.msdn.com/b/pfxteam/archive/2012/02/12/10266988.aspx" />
    public class AsyncLock
    {
        private readonly SemaphoreSlim semaphore;
        private readonly Task<LockSubscription> subscription;

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public AsyncLock()
        {
            semaphore = new SemaphoreSlim(1);
            subscription = Task.FromResult(new LockSubscription(this));
        }

        /// <summary>
        ///     Creates a async <c>lock</c> using the <see cref="TaskScheduler.Default" /> that will exist until the returned
        ///     <see cref="LockSubscription" /> gets disposed.
        /// </summary>
        /// <returns><see cref="LockSubscription" /> responsible for releasing the async lock.</returns>
        public Task<LockSubscription> LockAsync()
        {
            return LockAsync(TaskScheduler.Default);
        }

        /// <summary>
        ///     Creates a async <c>lock</c> using the <paramref name="taskScheduler" /> that will exist until the returned
        ///     <see cref="LockSubscription" /> gets disposed.
        /// </summary>
        /// <returns><see cref="LockSubscription" /> responsible for releasing the async lock.</returns>
        public Task<LockSubscription> LockAsync(TaskScheduler taskScheduler)
        {
            if (taskScheduler == null)
            {
                throw new ArgumentNullException(nameof(taskScheduler));
            }
            var wait = semaphore.WaitAsync();
            return wait.IsCompleted
                       ? subscription
                       : wait.ContinueWith((_, state) => new LockSubscription((AsyncLock) state),
                                           this,
                                           CancellationToken.None,
                                           TaskContinuationOptions.ExecuteSynchronously,
                                           taskScheduler);
        }


        #region LockSubscription structure ...

        /// <summary>
        ///     Structure responsible for releasing an <see cref="AsyncLock"/> when this subscription gets release.
        /// </summary>
        public struct LockSubscription : IDisposable
        {
            private readonly AsyncLock asyncLock;

            /// <summary>
            ///     <c>Internal</c> constructor that receives the <see cref="AsyncLock" /> to manager.
            /// </summary>
            /// <param name="asyncLock">The <see cref="AsyncLock" /> to be managed.</param>
            internal LockSubscription(AsyncLock asyncLock) : this()
            {
                if (asyncLock == null)
                {
                    throw new ArgumentNullException(nameof(asyncLock));
                }
                this.asyncLock = asyncLock;
            }

            #region IDisposable implementation...

            private bool disposed;

            public void Dispose()
            {
                Dispose(true);
            }

            private void Dispose(bool disposing)
            {
                if (disposed || !disposing)
                {
                    return;
                }
                disposed = true;
                lock (asyncLock)
                {
                    asyncLock?.semaphore.Release();
                }
            }

            #endregion
        }

        #endregion
    }
}
