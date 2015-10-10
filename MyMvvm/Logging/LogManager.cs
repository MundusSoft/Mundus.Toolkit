using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvvm.Logging
{
    /// <summary>
    /// Logging API for this framework. The user can inject his own implementation by setting the <see cref="LogManager.LogFactory"/> static property.
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// Get or set the log factory to be used.
        /// </summary>
        public static ILogFactory LogFactory { get; set; } = new NoLogFactory();

        /// <summary>
        /// Gets the <see cref="ILog" /> implementation that will be use for logging messages.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that will be used to identify the logged messages.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        public static ILog GetLogger(Type type)
        {
            return LogFactory.GetLogger(type);
        }

        /// <summary>
        /// Gets the <see cref="ILog"/> implementation that will be use for logging messages.
        /// </summary>
        /// <param name="typeName">A string that will be used to identify the logged messages.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        public static ILog GetLogger(string typeName)
        {
            return LogFactory.GetLogger(typeName);
        }

    }
}
