using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyMvvm.Threading
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
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="function">The action to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> the <paramref name="function"/> will be run.</param>
        /// <returns>
        /// A <see cref="Task" /> for the running operation.
        /// </returns>
        Task InvokeOnUIThreadAsync(Func<Task> function, DispatcherPriority priority);

    }
}
