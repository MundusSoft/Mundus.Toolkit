namespace MyMvvm.Threading
{
    /// <summary>
    ///     Defines the priorities at which operations can be invoked by way of the <see cref="IDispatcher" />.
    /// </summary>
    public enum DispatcherPriority
    {
        /// <summary>
        ///     Lowest priority. Use this priority for background tasks. Delegates are processed when the window's main thread is
        ///     idle and there is no input pending in the queue.
        /// </summary>
        Idle = -2,

        /// <summary>
        ///     Low priority. Delegates are processed if there are no higher priority events pending in the queue.
        /// </summary>
        Low = -1,

        /// <summary>
        ///     Normal priority. Delegates are processed in the order they are scheduled.
        /// </summary>
        Normal = 0,

        /// <summary>
        ///     High priority. Delegates are invoked immediately for all synchronous requests. Asynchronous requests are queued and
        ///     processed before any other request type.
        ///     <para>
        ///         <remarks>
        ///             <c>Caution</c>  Do not use this priority level in your app. It is reserved for system events. Using this
        ///             priority can lead to the starvation of other messages, including system events.
        ///         </remarks>
        ///     </para>
        /// </summary>
        High = 1,
    }
}
