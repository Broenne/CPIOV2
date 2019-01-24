namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Collections.Generic;

    using ConfigLogicLayer.Contracts.ActualId;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    public class ResetFlipFlop : IResetFlipFlop
    {
        #region Constructor

        public ResetFlipFlop(ILogger logger, IWriteBasicCan writeBasicCan, IGetActualNodeId actualNodeId)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
            this.GetActualNodeId = actualNodeId;
        }

        #endregion

        #region Properties

        private IGetActualNodeId GetActualNodeId { get; }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        public void ResetAll()
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                var data = new List<byte>();
                data.Add(0xFF);
                data.Add(0xFf);

                this.WriteBasicCan.WriteCan(this.GetActualNodeId.Get() + (uint)0x172, data);

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