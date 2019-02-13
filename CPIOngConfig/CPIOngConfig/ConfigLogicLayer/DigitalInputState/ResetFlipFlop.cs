namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Collections.Generic;

    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.DigitalInputState;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    /// The reset flip flop service.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.DigitalInputState.IResetFlipFlop" />
    public class ResetFlipFlop : IResetFlipFlop
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetFlipFlop"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        /// <param name="actualNodeId">The actual node identifier.</param>
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

        /// <summary>
        /// Resets all.
        /// </summary>
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

        public void ResetChannel(uint channel)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                var data = new List<byte>();

                var mask = 1 << (int)channel;
                var intBytes = BitConverter.GetBytes(mask);

                data.Add(intBytes[0]);
                data.Add(intBytes[1]);

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