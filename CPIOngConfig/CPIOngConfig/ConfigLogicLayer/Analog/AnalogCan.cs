namespace ConfigLogicLayer.Analog
{
    using System;
    using System.Collections.Generic;

    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Analog;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The service for analog can.
    /// </summary>
    public class AnalogCan : IAnalogCan
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnalogCan" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public AnalogCan(ILogger logger, IGetActualNodeId getActualNodeId, IWriteBasicCan writeBasicCan)
        {
            this.Logger = logger;
            this.GetActualNodeId = getActualNodeId;
            this.WriteBasicCan = writeBasicCan;
        }

        #endregion

        #region Properties

        private IGetActualNodeId GetActualNodeId { get; }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Triggers the specified channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void Trigger(uint channel)
        {
            try
            {
                var nodeId = this.GetActualNodeId.Get();
                var cobId = Convert.ToUInt32(nodeId + 0x42);
                var data = new List<byte>();
                data.Add(Convert.ToByte(channel));

                this.WriteBasicCan.WriteCan(cobId, data);
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