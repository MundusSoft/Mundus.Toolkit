using System;

namespace MyMvvm.Logging
{
    /// <summary>
    ///     Logger class that will log messages into the console.
    /// </summary>
    public class ConsoleLogger : ILog
    {
        private readonly LogLevel logLevel;
        private string typeName;

        #region constructors...

        /// <summary>
        ///     Creates a new console logger for the given <paramref name="type" /> with a log level of
        ///     <see cref="LogLevel.Info" />
        /// </summary>
        public ConsoleLogger(Type type) : this(type.FullName)
        {
        }

        /// <summary>
        ///     Creates a new console logger for the given <paramref name="typeName" /> with a log level of
        ///     <see cref="LogLevel.Info" />
        /// </summary>
        public ConsoleLogger(string typeName) : this(typeName, LogLevel.Info)
        {
        }

        /// <summary>
        ///     Creates a new console logger for the given <paramref name="type" /> with a log level of <paramref name="level" />
        /// </summary>
        public ConsoleLogger(Type type, LogLevel level) : this(type.FullName, level)
        {
        }

        /// <summary>
        ///     Creates a new console logger for the given <paramref name="typeName" /> with a log level of
        ///     <paramref name="level" />
        /// </summary>
        public ConsoleLogger(string typeName, LogLevel level)
        {
            this.typeName = typeName;
            this.logLevel = level;
        }

        #endregion

        /// <summary>
        ///     Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Trace" /> messages.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance should log <see cref="LogLevel.Trace" /> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsTraceEnabled => logLevel <= LogLevel.Trace;

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Trace" /> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Trace(string message)
        {
             Log(LogLevel.Trace, message);
        }

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Trace" /> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Trace(string message, Exception exception)
        {
            Log(LogLevel.Trace, message, exception);
        }

        /// <summary>
        ///     Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Debug" /> messages.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance should log <see cref="LogLevel.Debug" /> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsDebugEnabled => logLevel <= LogLevel.Debug;

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Debug" /> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Debug" /> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Debug(string message, Exception exception)
        {
            Log(LogLevel.Debug, message, exception);
        }

        /// <summary>
        ///     Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Info" /> messages.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance should log <see cref="LogLevel.Info" /> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsInfoEnabled => logLevel <= LogLevel.Info;

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Info" /> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Info" /> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Info(string message, Exception exception)
        {
            Log(LogLevel.Info, message, exception);
        }

        /// <summary>
        ///     Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Warn" /> messages.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance should log <see cref="LogLevel.Warn" /> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsWarnEnabled => logLevel <= LogLevel.Warn;

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Warn" /> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Warn(string message)
        {
            Log(LogLevel.Warn, message);
        }

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Warn" /> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Warn(string message, Exception exception)
        {
            Log(LogLevel.Warn, message, exception);
        }

        /// <summary>
        ///     Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Error" /> messages.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance should log <see cref="LogLevel.Error" /> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrorEnabled => logLevel <= LogLevel.Error;

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Error" /> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Error" /> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Error(string message, Exception exception)
        {
            Log(LogLevel.Error, message, exception);
        }

        /// <summary>
        ///     Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Fatal" /> messages.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance should log <see cref="LogLevel.Fatal" /> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsFatalEnabled => logLevel <= LogLevel.Fatal;

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Fatal" /> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Fatal(string message)
        {
            Log(LogLevel.Fatal, message);
        }

        /// <summary>
        ///     Logs a  <see cref="LogLevel.Fatal" /> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Fatal(string message, Exception exception)
        {
            Log(LogLevel.Fatal, message, exception);
        }

        private void Log(LogLevel level, string message, Exception exception = null)
        {
            if (logLevel > level)
            {
                return;
            }
            Console.WriteLine("{0:s} | {1} | {2}", DateTime.Now, level, message);
            if (exception == null)
            {
                return;
            }
            Console.WriteLine("    {0}", exception);
        }
    }
}
