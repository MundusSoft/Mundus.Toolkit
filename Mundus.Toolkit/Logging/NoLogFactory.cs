using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MToolkit.Logging
{
    /// <summary>
    /// Log factory class 
    /// </summary>
    public class NoLogFactory: ILogFactory
    {
        /// <summary>
        /// Gets the <see cref="ILog"/> implementation for this factory.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> whose <see cref="Type.Name"/> that will be used to identify the messages logged.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        public ILog GetLogger(Type type)
        {
            return new NoLogLogger();
        }

        /// <summary>
        /// Gets the <see cref="ILog"/> implementation for this factory.
        /// </summary>
        /// <param name="typeName">A string that will be used to identify the log messages.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        public ILog GetLogger(string typeName)
        {
            return new NoLogLogger();
        }
    }
}
