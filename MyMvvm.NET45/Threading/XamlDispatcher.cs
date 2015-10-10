using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MyMvvm.Threading
{
    /// <summary>
    ///     A <see cref="IDispatcher" /> implementation for the XAML 4.5+ platform.
    /// </summary>
    public class XamlDispatcher : IDispatcher
    {
        private Dispatcher dispatcher => Dispatcher.CurrentDispatcher;

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
            await dispatcher.InvokeAsync(action, execPriority);
        }

        /// <summary>
        /// Executes the function on the UI thread asynchronously with <see cref="DispatcherPriority.Normal"></see>.
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
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <returns>
        /// A <see cref="System.Threading.Tasks.Task" /> for the running operation.
        /// </returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority)
        {
            ValidateDispatcher();
            var execPriority = GetDispatcherPriority(priority);
            await dispatcher.InvokeAsync(function, execPriority);
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

        private System.Windows.Threading.DispatcherPriority GetDispatcherPriority(DispatcherPriority priority)
        {
            switch (priority)
            {
                case DispatcherPriority.Idle:
                    return System.Windows.Threading.DispatcherPriority.ApplicationIdle;
                case DispatcherPriority.Low:
                    return System.Windows.Threading.DispatcherPriority.Background;
                case DispatcherPriority.Normal:
                    return System.Windows.Threading.DispatcherPriority.Normal;
                case DispatcherPriority.High:
                    return System.Windows.Threading.DispatcherPriority.Send;
                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
            }
        }
        #endregion
    }
}
