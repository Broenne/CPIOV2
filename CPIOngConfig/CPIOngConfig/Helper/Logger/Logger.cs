namespace Helper.Logger
{
    using System;
    using System.Runtime.CompilerServices;

    using NLog;

    using ILogger = Helper.Contracts.Logger.ILogger;

    /// <summary>
    /// The logging service.
    /// </summary>
    public class Logger : ILogger
    {
        #region Private Consts

        private const string LogEventPropertyTestingNumber = "TestingNumber";

        #endregion

        #region Private Fields

        private readonly NLog.Logger logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger" /> class.
        /// </summary>
        /// <param name="applicationEnvironment">The application environment.</param>
        /// <exception cref="System.NullReferenceException">NLog logger is null.</exception>
        /// <exception cref="LoggerException">The Logger exception.</exception>
        public Logger(/*IApplicationEnvironment applicationEnvironment*/)
        {
            try
            {
                //this.ApplicationEnvironment = applicationEnvironment;

                this.logger = LogManager.GetLogger(string.Empty);
                if (this.logger == null)
                {
                    throw new NullReferenceException("NLog logger is null!");
                }
            }
            //catch (BaseException)
            //{
            //    throw;
            //}
            catch (Exception ex)
            {
                //throw new LoggerException(ErrorCodeAssistants.LoggerException, ex);
            }
        }

        //#endregion

        //#region Private Properties

        //private IApplicationEnvironment ApplicationEnvironment { get; }

        private NLog.Logger NLogger => this.logger;

        #endregion 

        #region Public Functions

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogDebug(string message)
        {
            this.LogMessage(LogLevel.Debug, message);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogTrace(string message)
        {
            this.LogMessage(LogLevel.Trace, message);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="code">The code for ENUM.</param>
        /// <param name="message">The message.</param>
        public void LogError(Enum code, string message)
        {
            this.LogMessage(LogLevel.Error, code + message);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
            this.LogMessage(LogLevel.Info, message);
        }

        /// <summary>
        /// Logs an Exception as informational message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogInfo(Exception exception)
        {
            this.LogException(LogLevel.Info, exception);
        }

        /// <summary>
        /// Logs an Exception as error message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogError(Exception exception)
        {
            this.LogException(LogLevel.Error, exception);
        }

        /// <summary>
        /// The get caller.
        /// </summary>
        /// <param name="caller">
        /// The caller.
        /// </param>
        /// <returns>
        /// The <see cref="string"/> caller.
        /// </returns>
        public string GetCaller([CallerMemberName] string caller = null)
        {
            return caller;
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

        /// <summary>
        /// Sets the log event properties.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        private void SetLogEventProperties(LogEventInfo logEvent)
        {
            //logEvent.Properties[LogEventPropertyTestingNumber] = this.ApplicationEnvironment.GetIdentificationNumber();
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The message to log.</param>
        private void LogMessage(LogLevel level, string message)
        {
            var logEvent = new LogEventInfo(level, this.NLogger.Name, message);
            this.SetLogEventProperties(logEvent);
            this.NLogger.Log(logEvent);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="ex">The exception to log.</param>
        private void LogException(LogLevel level, Exception ex)
        {
            var logEvent = new LogEventInfo(level, this.NLogger.Name, string.Empty) { Exception = ex };
            this.SetLogEventProperties(logEvent);
            this.NLogger.Log(logEvent);
        }

        #endregion
    }
}
