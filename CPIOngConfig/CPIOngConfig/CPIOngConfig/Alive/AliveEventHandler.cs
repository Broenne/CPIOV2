namespace CPIOngConfig.Alive
{
    using System;

    using CPIOngConfig.Contracts.Alive;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The input binary event handler.
    /// </summary>
    /// <seealso cref="IAliveEventHandler" />
    public class AliveEventHandler : IAliveEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AliveEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public AliveEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<AliveEventArgs> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="AliveEventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(AliveEventArgs e)
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