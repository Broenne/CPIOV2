namespace ConfigLogicLayer.DigitalInputState
{
    using System;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    /// The service for stimulate.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.DigitalInputState.IStimulate" />
    public class Stimulate : IStimulate
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Stimulate" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public Stimulate(ILogger logger, IWriteBasicCan writeBasicCan)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
        }

        #endregion

        #region Properties

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Requests the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RequestById(uint id)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                this.WriteBasicCan.RemoteRequestForChannelValue(id);
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