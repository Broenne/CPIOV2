namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Collections.Generic;

    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.ConfigInputs;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The set active sensor.
    /// </summary>
    public class SetActiveSensorToDetect : ISetActiveSensorToDetect
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="SetActiveSensorToDetect" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        public SetActiveSensorToDetect(ILogger logger, IWriteBasicCan writeBasicCan, IGetActualNodeId getActualNodeId)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
            this.GetActualNodeId = getActualNodeId;
        }

        #endregion

        #region Properties

        private IGetActualNodeId GetActualNodeId { get; }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Does the specified modi.
        /// </summary>
        /// <param name="modi">The modi info.</param>
        public void Do(Modi modi)
        {
            try
            {
                const uint SelectPulseInput = 512;
                var id = this.GetActualNodeId.Get();
                var data = new List<byte>();
                data.Add((byte)modi);

                var cobid = SelectPulseInput + id;
                this.WriteBasicCan.WriteCan(cobid, data);
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