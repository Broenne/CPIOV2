namespace Helper.Logger
{
    using System;
    using System.Runtime.CompilerServices;

    using Helper.Contracts.Logger;

    /// <summary>
    /// The logging service.
    /// </summary>
    public class Logger : ILogger
    {
       


       
        #region Public Functions

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogDebug(string message)
        {
            
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogTrace(string message)
        {
            
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="code">The code for ENUM.</param>
        /// <param name="message">The message.</param>
        public void LogError(Enum code, string message)
        {
            
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
            
        }

        /// <summary>
        /// Logs an Exception as informational message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogInfo(Exception exception)
        {
            
        }

        /// <summary>
        /// Logs an Exception as error message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogError(Exception exception)
        {
            
        }

       

        /// <summary>
        /// The log begin.
        /// </summary>
        /// <param name="type">
        /// The type of the calling class.
        /// </param>
        /// <param name="caller">
        /// The caller.
        /// </param>
        public void LogBegin(Type type, [CallerMemberName] string caller = null)
        {
            var typeName = string.Empty;

            if (type != null)
            {
                typeName = type.ToString();
            }

            this.LogInfo($"Begin: {typeName}.{caller}");
        }

        /// <summary>
        /// The log end.
        /// </summary>
        /// <param name="type">
        /// The type of the calling class.
        /// </param>
        /// <param name="caller">
        /// The caller.
        /// </param>
        public void LogEnd(Type type, [CallerMemberName] string caller = null)
        {
            var typeName = string.Empty;

            if (type != null)
            {
                typeName = type.ToString();
            }

            this.LogInfo($"End: {typeName}, {caller}");
        }

        #endregion

        #region Private Functions

       

        #endregion
    }
}
