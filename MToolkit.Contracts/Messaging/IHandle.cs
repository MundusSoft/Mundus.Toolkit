using System.Threading.Tasks;

namespace MyMvvm.Messaging
{
    /// <summary>
    ///     An <see langword="interface" /> for classes that subscribe to messages.
    /// </summary>
    public interface IHandle
    {
    }

    /// <summary>
    ///     A class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name="TMessage">
    ///     The type of message to handle. Types that inherit from <typeparamref name="TMessage" /> will
    ///     also be handle.
    /// </typeparam>
    public interface IHandle<in TMessage> : IHandle
    {
        /// <summary>
        ///     Handles the <paramref name="message" />.
        /// </summary>
        /// <param name="message">The published message.</param>
        void Handle(TMessage message);
    }
}
