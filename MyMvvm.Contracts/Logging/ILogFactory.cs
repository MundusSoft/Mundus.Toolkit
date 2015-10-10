using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvvm.Logging
{
    /// <summary>
    /// Factory responsible for creating ILog instances.
    /// </summary>
    public interface ILogFactory
    {
        /// <summary>
        /// Gets the <see cref="ILog"/> implementation for this factory.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that will be used to identify the messages logged.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        ILog GetLogger(Type type);

        /// <summary>
        /// Gets the <see cref="ILog"/> implementation for this factory.
        /// </summary>
        /// <param name="typeName">A string that will be used to identify the logged messages.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        ILog GetLogger(string typeName);
    }
}
