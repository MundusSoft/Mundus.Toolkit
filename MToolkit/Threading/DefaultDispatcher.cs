using System;
using System.Threading;
using System.Threading.Tasks;

namespace MToolkit.Threading
{
    /// <summary>
    /// Default implementation for <see cref="IDispatcher"/> that isn't bound to any platform.
    /// </summary>
    public class DefaultDispatcher : IDispatcher
    {
        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>The running <see cref="Task"/>.</returns>
        public async Task InvokeOnUIThreadAsync(Action action)
        {
            await Task.Yield();
            action();
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="action"/> will be run.</param>
        /// <returns>
        /// A <see cref="Task" /> for the running operation.
        /// </returns>
        public async Task InvokeOnUIThreadAsync(Action action, DispatcherPriority priority)
        {
            await Task.Yield();
            action();
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The action to execute.</param>
        /// <returns>The running <see cref="Task"/>.</returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function)
        {
            await function();
        }

        /// <summary>
        /// Executes the function on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <param name="token">The <see cref="CancellationToken"/> that will be passed into the <paramref name="function"/></param>
        /// <returns>The running <see cref="Task"/>.</returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function, CancellationToken token)
        {
            await function();
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The action to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <returns>
        /// A <see cref="Task" /> for the running operation.
        /// </returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority)
        {
            await function();
        }

        /// <summary>
        /// Executes the function on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// A <see cref="Task" /> for the running operation.
        /// </returns>
        public async Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority, CancellationToken token)
        {
            await function();
        }
    }
}
