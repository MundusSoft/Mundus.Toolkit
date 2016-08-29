using System.Threading.Tasks;

namespace MToolkit.Messaging
{
    /// <summary>
    ///     An <see langword="interface" /> for classes that subscribe to messages.
    /// </summary>
    public interface IHandleAsync
    {
    }

    /// <summary>
    ///     A class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name="TMessage">
    ///     The type of message to handle. Types that inherit from <typeparamref name="TMessage" /> will
    ///     also be handle.
    /// </typeparam>
    public interface IHandleAsync<in TMessage> : IHandleAsync
    {
        /// <summary>
        ///     Handles the <paramref name="message" /> Asynchronous.
        /// </summary>
        /// <param name="message">The publish message.</param>
        Task HandleAsync(TMessage message);
    }
}
