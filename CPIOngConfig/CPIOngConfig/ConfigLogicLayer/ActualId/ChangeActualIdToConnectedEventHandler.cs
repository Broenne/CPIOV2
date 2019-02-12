namespace ConfigLogicLayer.ActualId
{
    using System;

    using ConfigLogicLayer.Contracts.ActualId;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The change actual connected event handler node id.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.ActualId.IChangeActualIdToConnectedEventHandler" />
    public class ChangeActualIdToConnectedEventHandler : IChangeActualIdToConnectedEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangeActualIdToConnectedEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ChangeActualIdToConnectedEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<byte> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        public virtual void OnReached(byte e)
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