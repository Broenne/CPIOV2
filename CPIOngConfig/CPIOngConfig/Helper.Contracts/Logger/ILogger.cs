namespace Helper.Contracts.Logger
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     The logger interface.
    /// </summary>
    public interface ILogger
    {
        #region Public Methods

        /// <summary>
        /// Logs the begin.
        /// </summary>
        /// <param name="type">The type for log.</param>
        /// <param name="caller">The caller.</param>
        void LogBegin(Type type, [CallerMemberName] string caller = null);

        /// <summary>
        ///     Logs the debug.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs the end.
        /// </summary>
        /// <param name="type">The type for log..</param>
        /// <param name="caller">The caller.</param>
        void LogEnd(Type type, [CallerMemberName] string caller = null);

        /// <summary>
        ///  Logs the error.
        /// </summary>
        /// <param name="ex">The exception.</param>
        void LogError(Exception ex);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="code">The code for ENUM.</param>
        /// <param name="message">The message.</param>
        void LogError(Enum code, string message);

        /// <summary>
        ///     Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogTrace(string message);

        #endregion
    }
}