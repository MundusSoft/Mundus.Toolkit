using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace MToolkit.Threading
{
    /// <summary>
    ///  A <see cref="IDispatcher" /> implementation for the Windows Universal applications.
    /// </summary>
    public class Dispatcher : IDispatcher

    {
        private CoreDispatcher dispatcher => Window.Current.Dispatcher;

        #region public methods...

        /// <summary>
        ///     Executes the action on the UI thread asynchronously with <see cref="DispatcherPriority.Normal"/>.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>The running <see cref="System.Threading.Tasks.Task" />.</returns>
        public async Task InvokeOnUIThreadAsync(Action action)
        {
            await InvokeOnUIThreadAsync(action, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="action"/> will be run.</param>
        /// <returns>
        /// A <see cref="System.Threading.Tasks.Task" /> for the running operation.
        /// </returns>
        public async Task InvokeOnUIThreadAsync(Action action, DispatcherPriority priority)
        {
              ValidateDispatcher();
            var execPriority = GetDispatcherPriority(priority);
            await dispatcher.RunAsync(execPriority,()=> action());
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously with <see cref="DispatcherPriority.Normal"></see>.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <returns>The running <see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function)
        {
            await InvokeOnUIThreadAsync(function, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Executes the function on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <param name="token">The <see cref="System.Threading.CancellationToken"/> that will be passed into the <paramref name="function"/></param>
        /// <returns>The running <see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function, CancellationToken token)
        {
            await InvokeOnUIThreadAsync(function, DispatcherPriority.Normal, token);
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <returns>
        /// A <see cref="System.Threading.Tasks.Task" /> for the running operation.
        /// </returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority)
        {
            ValidateDispatcher();
            var execPriority = GetDispatcherPriority(priority);
            await dispatcher.RunTaskAsync(async() => await function(), execPriority);
        }

        /// <summary>
        /// Executes the function on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <param name="token">The <see cref="System.Threading.CancellationToken"/> that will be passed into the <paramref name="function"/></param>
        /// <returns>
        /// A <see cref="System.Threading.Tasks.Task" /> for the running operation.
        /// </returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority, CancellationToken token)
        {
            ValidateDispatcher();
            var execPriority = GetDispatcherPriority(priority);
            await dispatcher.RunTaskAsync(async () => await function(), execPriority);
        }

        #endregion

        #region private methods...

        private void ValidateDispatcher()
        {
            if (dispatcher == null)
            {
                throw new InvalidOperationException("Not initialized with dispatcher.");
            }
        }

        private CoreDispatcherPriority GetDispatcherPriority(DispatcherPriority priority)
        {
            switch (priority)
            {
                case DispatcherPriority.Idle:
                    return CoreDispatcherPriority.Idle;
                case DispatcherPriority.Low:
                    return CoreDispatcherPriority.Low;
                case DispatcherPriority.Normal:
                    return CoreDispatcherPriority.Normal;
                case DispatcherPriority.High:
                    return CoreDispatcherPriority.High;
                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
            }
        }
        #endregion
    }
        internal static class DispatcherTaskExtensions
        {
            private static async Task<T> RunTaskAsync<T>(this CoreDispatcher dispatcher,
                Func<Task<T>> func, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal, CancellationToken token = new CancellationToken())
            {
                var taskCompletionSource = new TaskCompletionSource<T>();

                await dispatcher.RunAsync(priority, async () =>
                {
                    try
                    {
                        taskCompletionSource.SetResult(await func());
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.SetException(ex);
                    }
                });
                return await taskCompletionSource.Task;
            }

            // There is no TaskCompletionSource<void> so we use a bool that we throw away.
            internal static async Task RunTaskAsync(this CoreDispatcher dispatcher,
                Func<Task> func, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal) =>
                await RunTaskAsync(dispatcher, async () => { await func(); return false; }, priority);
        }
}
