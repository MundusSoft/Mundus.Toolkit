using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MToolkit.Threading;

namespace MToolkit.Messaging
{
    /// <summary>
    ///     Implementation of <see cref="IMessageAggregator" /> that enables
    ///     loosely-coupled publication of and subscription to events asynchronous.
    /// </summary>
    public class MessageAggregator : IMessageAggregator
    {
        #region internal Handler class

        /// <summary>
        ///     Private handler class that is responsible for handling event subscriptions of a subscriber.
        ///     The subscriber is hold in a weak reference to prevent memory leaks.
        /// </summary>
        private class Handler
        {
            private readonly WeakReference reference;
            private readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

            /// <summary>
            ///     Gets a value indicating whether <see langword="this" /> subscriber is dead.
            /// </summary>
            /// <value>
            ///     true if <see langword="this" /> subscriber is dead; otherwise, false.
            /// </value>
            public bool IsDead => reference.Target == null;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="handler">The handler subscriber.</param>
            public Handler(object handler)
            {
                reference = new WeakReference(handler);

                handler.GetType().GetTypeInfo().ImplementedInterfaces
                       .Where(x =>
                              (typeof(IHandleAsync).GetTypeInfo().IsAssignableFrom(x.GetTypeInfo())
                               || typeof(IHandle).GetTypeInfo().IsAssignableFrom(x.GetTypeInfo()))
                              && x.GetTypeInfo().IsGenericType)
                       .ForEach(@interface =>
                                {
                                    var type = @interface.GetTypeInfo().GenericTypeArguments[0];
                                    var method = @interface.GetRuntimeMethod("HandleAsync", new[] {type})
                                                 ?? @interface.GetRuntimeMethod("Handle", new[] {type});
                                    supportedHandlers[type] = method;
                                });
            }

            /// <summary>
            ///     Determines if the specified <paramref name="instance" /> is the current subscriber.
            /// </summary>
            /// <param name="instance">The instance to be check.</param>
            /// <returns>
            ///     true if <paramref name="instance" /> is being handled by this handle; otherwise, false.
            /// </returns>
            public bool Matches(object instance)
            {
                return reference.Target == instance;
            }

            /// <summary>
            ///     Handles the specified message type asynchronous.
            /// </summary>
            /// <param name="messageType"><see cref="Type"/> of the message to be handled.</param>
            /// <param name="message">The publish message.</param>
            /// <returns>
            ///     true if this instance subscription is still available and it handled the message; otherwise, false.
            /// </returns>
            public async Task<bool> HandleAsync(Type messageType, object message)
            {
                var target = reference.Target;
                if (target == null)
                {
                    return false;
                }

                var runningTasks = supportedHandlers
                    .Where(pair =>
                           pair.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()))
                    .Select(pair => pair.Value.Invoke(target, new[] {message})).OfType<Task>()
                    .Where(t => t != null).ToList();
                while (runningTasks.Count > 0)
                {
                    var finishTask = await Task.WhenAny(runningTasks);
                    runningTasks.Remove(finishTask);
                }

                return true;
            }

            /// <summary>
            ///     Determines if this instance can handle the specified <paramref name="messageType" />.
            /// </summary>
            /// <param name="messageType">Type of the message to be handled.</param>
            /// <returns>
            ///     true if this instance can handle the <paramref name="messageType" /> ; otherwise, false.
            /// </returns>
            public bool Handles(Type messageType)
            {
                return supportedHandlers.Any(pair => pair.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()));
            }
        }

        #endregion

        #region fields...

        private readonly AsyncLock asyncLock = new AsyncLock();
        private readonly List<Handler> handlers = new List<Handler>();

        #endregion

        #region IMessageAggregator implementation...

        /// <summary>
        ///     Searches the subscribed handlers to check if we have a handler for
        ///     the message type supplied, inherit types of <paramref name="messageType" /> are also consider.
        /// </summary>
        /// <param name="messageType">The message type to check with</param>
        /// <returns>true if any handler is found, false if not.</returns>
        public bool HandlerExistsFor(Type messageType)
        {
            if (messageType == null)
            {
                throw new ArgumentNullException(nameof(messageType));
            }
            return handlers.Any(handler => !handler.IsDead & handler.Handles(messageType));
        }

        /// <summary>
        ///     Subscribes an instance to all events declared through implementations of <see cref="IHandleAsync{TMessage}" />
        ///     asynchronous.
        /// </summary>
        /// <param name="subscriber">The instance to subscribe for event publication.</param>
        public async Task SubscribeAsync(object subscriber)
        {
            await SubscribeAsync(subscriber, CancellationToken.None);
        }

        /// <summary>
        /// Subscribes an instance to all events declared through implementations of <see cref="IHandleAsync{TMessage}" /> asynchronous.
        /// </summary>
        /// <param name="subscriber">The instance to subscribe for event publication.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public async Task SubscribeAsync(object subscriber, CancellationToken token)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }
            using (await GetLockAsync(token))
            {
                var handler = handlers.FirstOrDefault(x => x.Matches(subscriber));
                if (handler != null)
                {
                    return;
                }
                handler = new Handler(subscriber);
                handlers.Add(handler);
            }
        }

        /// <summary>
        ///     Unsubscribe the instance from all events asynchronous.
        /// </summary>
        /// <param name="subscriber">The instance to unsubscribe.</param>
        public async Task UnsubscribeAsync(object subscriber)
        {
            await UnsubscribeAsync(subscriber, CancellationToken.None);
        }

        /// <summary>
        ///   Unsubscribe the instance from all events asynchronous.
        /// </summary>
        /// <param name = "subscriber">The instance to unsubscribe.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        public async Task UnsubscribeAsync(object subscriber, CancellationToken token)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }
            using (await GetLockAsync(token))
            {
                var handler = handlers.FirstOrDefault(x => x.Matches(subscriber));

                if (handler == null || token.IsCancellationRequested)
                {
                    return;
                }
                handlers.Remove(handler);
            }
        }

        /// <summary>
        ///     Publishes a message on the current thread asynchronous.
        /// </summary>
        /// <param name="message">The message instance.</param>
        public async Task PublishAsync(object message)
        {
            await PublishAsync(message, CancellationToken.None);
        }

        /// <summary>
        ///   Publishes a message on the current thread asynchronous.
        /// </summary>
        /// <param name = "message">The message instance.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        public async Task PublishAsync(object message, CancellationToken token)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            Handler[] toNotify;
            using (await GetLockAsync(token))
            {
                toNotify = handlers.ToArray();
            }
            if (token.IsCancellationRequested)
            {
                return;
            }
            var messageType = message.GetType();
            var dead = new ConcurrentBag<Handler>();
            await toNotify.ForEachAsync(async handler =>
                                              {
                                                  if (!await handler.HandleAsync(messageType, message))
                                                  {
                                                      if (token.IsCancellationRequested)
                                                      {
                                                          return;
                                                      }
                                                      dead.Add(handler);
                                                  }
                                              },
                                        token);
            if (!dead.Any() )
            {
                return;
            }
            using (await GetLockAsync(token))
            {
                dead.ForEach(x => handlers.Remove(x));
            }
        }

        #endregion

        #region private methods...

        /// <summary>
        ///     Gets a asynchronous lock using <see cref="TaskScheduler.Default" />.
        /// </summary>
        /// <returns>An awaitable <see cref="Task{T}" /> of <see cref="AsyncLock.LockSubscription" /></returns>
        private async Task<AsyncLock.LockSubscription> GetLockAsync()
        {
            return await GetLockAsync(TaskScheduler.Default).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a asynchronous lock using <see cref="TaskScheduler.Default" />.
        /// </summary>
         /// <param name="token">The <see cref="CancellationToken"/>.</param>
       /// <returns>An awaitable <see cref="Task{T}" /> of <see cref="AsyncLock.LockSubscription" /></returns>
        private async Task<AsyncLock.LockSubscription> GetLockAsync(CancellationToken token)
        {
            return await GetLockAsync(TaskScheduler.Default, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a asynchronous lock.
        /// </summary>
        /// <param name="taskScheduler">A <see cref="TaskScheduler" />.</param>
        /// <returns>An awaitable <see cref="Task{T}" /> of <see cref="AsyncLock.LockSubscription" /></returns>
        private async Task<AsyncLock.LockSubscription> GetLockAsync(TaskScheduler taskScheduler)
        {
            return await GetLockAsync(taskScheduler, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a asynchronous lock.
        /// </summary>
        /// <param name="taskScheduler">A <see cref="TaskScheduler" />.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// An awaitable <see cref="Task{T}" /> of <see cref="AsyncLock.LockSubscription" />
        /// </returns>
        private async Task<AsyncLock.LockSubscription> GetLockAsync(TaskScheduler taskScheduler, CancellationToken token)
        {
            return await asyncLock.LockAsync(taskScheduler, token).ConfigureAwait(false);
        }

        #endregion
    }
}
