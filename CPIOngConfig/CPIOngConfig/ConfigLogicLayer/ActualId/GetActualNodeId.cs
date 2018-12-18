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
        /// Initializes a new instance of the <see cref="GetActualNodeId"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GetActualNodeId(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Return the can id.</returns>
        public byte Get()
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                return 4;
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
    }
}