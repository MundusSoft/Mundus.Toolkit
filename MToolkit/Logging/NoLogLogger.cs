using System;

namespace MyMvvm.Logging
{
    /// <summary>
    /// Logger class that doesn't not anything.
    /// </summary>
    /// <remarks>This class is used by default in the MyMvvm framework.</remarks>
    public class NoLogLogger : ILog
    {
        /// <summary>
        /// Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Trace"/> messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance should log <see cref="LogLevel.Trace"/> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsTraceEnabled { get; } = false;

        /// <summary>
        /// Logs a  <see cref="LogLevel.Trace"/> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Trace(string message)
        {
        }

        /// <summary>
        /// Logs a  <see cref="LogLevel.Trace"/> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Trace(string message, Exception exception)
        {
        }

        /// <summary>
        /// Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Debug"/> messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance should log <see cref="LogLevel.Debug"/> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsDebugEnabled { get; } = false;

        /// <summary>
        /// Logs a  <see cref="LogLevel.Debug"/> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Debug(string message)
        {
        }

        /// <summary>
        /// Logs a  <see cref="LogLevel.Debug"/> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Debug(string message, Exception exception)
        {
        }

        /// <summary>
        /// Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Info"/> messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance should log <see cref="LogLevel.Info"/> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsInfoEnabled { get; } = false;

        /// <summary>
        /// Logs a  <see cref="LogLevel.Info"/> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Info(string message)
        {
        }

        /// <summary>
        /// Logs a  <see cref="LogLevel.Info"/> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Info(string message, Exception exception)
        {
        }

        /// <summary>
        /// Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Warn"/> messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance should log <see cref="LogLevel.Warn"/> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsWarnEnabled { get; } = false;

        /// <summary>
        /// Logs a  <see cref="LogLevel.Warn"/> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Warn(string message)
        {
        }

        /// <summary>
        /// Logs a  <see cref="LogLevel.Warn"/> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Warn(string message, Exception exception)
        {
        }

        /// <summary>
        /// Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Error"/> messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance should log <see cref="LogLevel.Error"/> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrorEnabled { get; } = false;

        /// <summary>
        /// Logs a  <see cref="LogLevel.Error"/> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Error(string message)
        {
        }

        /// <summary>
        /// Logs a  <see cref="LogLevel.Error"/> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Error(string message, Exception exception)
        {
        }

        /// <summary>
        /// Gets a value indicating whether <c>this</c> instance should log <see cref="LogLevel.Fatal"/> messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance should log <see cref="LogLevel.Fatal"/> messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsFatalEnabled { get; } = false;

        /// <summary>
        /// Logs a  <see cref="LogLevel.Fatal"/> message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Fatal(string message)
        {
        }

        /// <summary>
        /// Logs a  <see cref="LogLevel.Fatal"/> message with an exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public void Fatal(string message, Exception exception)
        {
        }
    }
}
