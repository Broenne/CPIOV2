namespace CPIOngConfig.Pulse
{
    using System;

    using CPIOngConfig.Contracts.Pulse;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The pulse event handler.
    /// </summary>
    /// <seealso cref="CPIOngConfig.Pulse.IPulseEventHandler" />
    public class PulseEventHandler : IPulseEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PulseEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PulseEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<PulseEventArgs> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PulseEventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(PulseEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    throw new Exception("Event for pulse is null");
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