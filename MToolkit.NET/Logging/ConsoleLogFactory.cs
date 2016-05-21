using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMvvm.Configuration;

namespace MyMvvm.Logging
{
    /// <summary>
    /// Console Log factory class 
    /// </summary>
    public class ConsoleLogFactory: ILogFactory
    {
        private ISettings settings;

        /// <summary>
        /// Construct a new instance of <see cref="ConsoleLogFactory"/> class.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        public ConsoleLogFactory(ISettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Gets the <see cref="ILog"/> implementation for this factory.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that will be used to identify the messages logged.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        public ILog GetLogger(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return new ConsoleLogger(type);
        }

        /// <summary>
        /// Gets the <see cref="ILog"/> implementation for this factory.
        /// </summary>
        /// <param name="typeName">A string that will be used to identify the log messages.</param>
        /// <returns>The <see cref="ILog"/> implementation.</returns>
        public ILog GetLogger(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException(nameof(typeName));
            }
            return new ConsoleLogger(typeName);
        }
    }
}
