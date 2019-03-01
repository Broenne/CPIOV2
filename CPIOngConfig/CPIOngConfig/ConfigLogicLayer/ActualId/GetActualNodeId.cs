namespace ConfigLogicLayer.ActualId
{
    using System;

    using ConfigLogicLayer.Contracts.ActualId;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The get actual node ID.
    /// </summary>
    /// <seealso cref="IGetActualNodeId" />
    public class GetActualNodeId : IGetActualNodeId
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="GetActualNodeId" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="changeActualIdToConnectedEventHandler">The change actual identifier to connected event handler.</param>
        public GetActualNodeId(ILogger logger, IChangeActualIdToConnectedEventHandler changeActualIdToConnectedEventHandler)
        {
            this.Logger = logger;
            this.ChangeActualIdToConnectedEventHandler = changeActualIdToConnectedEventHandler;
            this.ChangeActualIdToConnectedEventHandler.EventIsReached += this.ChangeActualIdToConnectedEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        private static ushort NodeId { get; set; } = 724;

        private IChangeActualIdToConnectedEventHandler ChangeActualIdToConnectedEventHandler { get; }

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>Return the can id.</returns>
        public ushort Get()
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                return NodeId;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        #endregion

        #region Private Methods

        private void ChangeActualIdToConnectedEventHandler_EventIsReached(object sender, ushort e)
        {
            try
            {
                NodeId = e;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion
    }
}