using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvvm.Logging
{
    /// <summary>
    /// <c>Enum</c> with all the possible log levels.
    /// </summary>
    public enum LogLevel
    {
        All,
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal, 
        Off
    }
}
