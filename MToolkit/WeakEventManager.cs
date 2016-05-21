using System;

namespace MToolkit
{
    /// <summary>
    ///     Static Class that holds the extension methods to handle events using weak references and actions.
    ///     This way we don't need to worry about unregister the event handler meaning that.
    /// </summary>
    public static class WeakEventManager
    {
        public class WeakEventSubscription<TDelegate> : IDisposable
            where TDelegate : class
        {
            #region constructors...

            public WeakEventSubscription(Action<TDelegate> removeAction, TDelegate handler)
            {
                if (removeAction == null)
                {
                    throw new ArgumentNullException(nameof(removeAction));
                }
                if (handler == null)
                {
                    throw new ArgumentNullException(nameof(handler));
                }
                this.removeAction = removeAction;
                this.handler = handler;
            }

            #endregion

            #region fields...

            private Action<TDelegate> removeAction;
            private TDelegate handler;

            #endregion

            #region IDisposable implementation...

            private bool disposed;

            // Dispose() calls Dispose(true)
            public void Dispose()
            {
                if (disposed)
                {
                    return;
                }

                Dispose(true);
                disposed = true;
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposing)
                {
                    return;
                }

                removeAction(handler);
                removeAction = null;
                handler = null;
            }

            #endregion
        }

        /// <summary>
        ///     This overload handles any type of EventHandler
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <typeparam name="TDelegate">The type of the T delegate.</typeparam>
        /// <typeparam name="TArgs">The type of the T args.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="converter">The converter.</param>
        /// <param name="add">The add.</param>
        /// <param name="remove">The remove.</param>
        /// <param name="action">The action.</param>
        public static WeakEventSubscription<TDelegate> WeakSubscribe<T, TDelegate, TArgs>
            (this T subscriber,
             Func<EventHandler<TArgs>, TDelegate> converter,
             Action<TDelegate> add,
             Action<TDelegate> remove,
             Action<T, TArgs> action)
            where TArgs : EventArgs
            where TDelegate : class
            where T : class
        {
            var subsWeakRef = new WeakReference(subscriber);
            TDelegate[] handler =
            {
                null
            };
            handler[0] = converter((s, e) =>
                                   {
                                       var subsStrongRef = subsWeakRef.Target as T;
                                       if (subsStrongRef != null)
                                       {
                                           action(subsStrongRef, e);
                                       }
                                       else
                                       {
                                           if (handler[0] != null)
                                           {
                                               try
                                               {
                                                   remove(handler[0]);
                                               }
                                               catch (Exception)
                                               {
                                               }
                                           }
                                           handler[0] = null;
                                       }
                                   });
            add(handler[0]);

            return new WeakEventSubscription<TDelegate>(remove, handler[0]);
        }

        /// <summary>
        ///     this overload is simplified for generic EventHandlers
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <typeparam name="TArgs">The type of the T args.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="add">The add.</param>
        /// <param name="remove">The remove.</param>
        /// <param name="action">The action.</param>
        public static WeakEventSubscription<EventHandler<TArgs>> WeakSubscribe<T, TArgs>(this T subscriber,
                                                                                         Action<EventHandler<TArgs>> add,
                                                                                         Action<EventHandler<TArgs>>
                                                                                             remove,
                                                                                         Action<T, TArgs> action)
            where TArgs : EventArgs
            where T : class
        {
            return WeakSubscribe(subscriber,
                                 h => h,
                                 add,
                                 remove,
                                 action);
        }

        /// <summary>
        ///     this overload is simplified for EventHandlers.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="add">The add.</param>
        /// <param name="remove">The remove.</param>
        /// <param name="action">The action.</param>
        public static WeakEventSubscription<EventHandler> WeakSubscribe<T>(this T subscriber,
                                                                           Action<EventHandler> add,
                                                                           Action<EventHandler> remove,
                                                                           Action<T, EventArgs> action)
            where T : class
        {
            return WeakSubscribe(subscriber,
                                 h => (o, e) => h(o, e),
                                 //This is a workaround from Rx
                                 add,
                                 remove,
                                 action);
        }
    }
}
