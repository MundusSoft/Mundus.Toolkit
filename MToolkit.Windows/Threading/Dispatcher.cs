using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace MyMvvm.Threading
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
            await dispatcher.RunAsync(execPriority, async() => await function());
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
}
