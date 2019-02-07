namespace CPIOngConfig.ActiveSensor
{
    using System;

    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.ConfigInputs;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The input binary event handler.
    /// </summary>
    /// <seealso cref="IAliveEventHandler" />
    public class ActiveSensorEventHandler : IActiveSensorEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActiveSensorEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ActiveSensorEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<Modi> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="AliveEventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(Modi e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    return;
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