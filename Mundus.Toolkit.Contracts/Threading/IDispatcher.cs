using System;
using System.Threading;
using System.Threading.Tasks;

namespace MToolkit.Threading
{
    /// <summary>
    /// Interface for UI Dispatcher that will execute <seealso cref="Action"/> in the UI Thread.
    /// </summary>
   public interface IDispatcher
    {

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>The running <see cref="Task"/>.</returns>
        Task InvokeOnUIThreadAsync(Action action);

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="action"/> will be run.</param>
        /// <returns>
        /// A <see cref="Task" /> for the running operation.
        /// </returns>
        Task InvokeOnUIThreadAsync(Action action, DispatcherPriority priority);

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The action to execute.</param>
        /// <returns>The running <see cref="Task"/>.</returns>
        Task InvokeOnUIThreadAsync(Func<Task> function);

        /// <summary>
        /// Executes the function on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>The running <see cref="Task"/>.</returns>
        Task InvokeOnUIThreadAsync(Func<Task> function, CancellationToken token);

        /// <summary>
        /// Executes the function on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The action to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <returns>
        /// A <see cref="Task" /> for the running operation.
        /// </returns>
        Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority);

        /// <summary>
        /// Executes the function on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// A <see cref="Task" /> for the running operation.
        /// </returns>
        Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority, CancellationToken token);

    }
}
