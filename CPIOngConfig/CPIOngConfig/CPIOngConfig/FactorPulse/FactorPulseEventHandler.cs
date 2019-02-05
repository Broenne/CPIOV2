namespace CPIOngConfig.FactorPulse
{
    using System;

    using CPIOngConfig.Contracts.FactorPulse;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The factor pulse event handler.
    /// </summary>
    /// <seealso cref="CPIOngConfig.Contracts.FactorPulse.IFactorPulseEventHandler" />
    public class FactorPulseEventHandler : IFactorPulseEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="FactorPulseEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public FactorPulseEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [event is reached].
        /// </summary>
        public event EventHandler<FactorPulseEventArgs> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Called when [reached].
        /// </summary>
        /// <param name="e">The event argument.</param>
        public void OnReached(FactorPulseEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    throw new Exception("Event for pulse factor is null");
                }

                this.EventIsReached.Invoke(this, e);
            }
            catch (Exception exception)
            {
                this.Logger.LogError(exception);
                throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        #endregion
    }
}