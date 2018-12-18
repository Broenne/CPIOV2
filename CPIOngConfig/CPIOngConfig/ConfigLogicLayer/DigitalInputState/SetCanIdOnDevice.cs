namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Collections.Generic;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The service for set can device ID.
    /// </summary>
    public class SetCanIdOnDevice : ISetCanIdOnDevice
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="SetCanIdOnDevice" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public SetCanIdOnDevice(ILogger logger, IWriteBasicCan writeBasicCan)
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
        /// Does the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Do(byte id)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                this.Logger.LogDebug($"Set can id to {id}");
                var data = new List<byte>();
                data.Add(0x01);
                data.Add(id);

                this.WriteBasicCan.WriteCan(0, data);

                // todo mb: wait for alive
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