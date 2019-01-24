namespace CPIOngConfig.FlipFlop
{
    using System;

    using CPIOngConfig.Contracts.FlipFlop;
    using CPIOngConfig.Pulse;

    using Helper.Contracts.Logger;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="CPIOngConfig.Contracts.FlipFlop.IFlipFlopEventHandler" />
    public class FlipFlopEventHandler : IFlipFlopEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PulseEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public FlipFlopEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<FlipFlopEventArgs> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(FlipFlopEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    throw new Exception("Event for flip flop is null");
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