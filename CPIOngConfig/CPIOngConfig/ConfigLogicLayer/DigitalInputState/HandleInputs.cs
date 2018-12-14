namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Linq;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.Pulse;
    using CPIOngConfig.Pulse;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    /// Handle the darware inputs.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.DigitalInputState.IHandleInputs" />
    public class HandleInputs : IHandleInputs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleInputs"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pulseEventHandler">The pulse event handler.</param>
        /// <param name="readCanMessage">The read can message.</param>
        public HandleInputs(ILogger logger, IPulseEventHandler pulseEventHandler, IReadCanMessage readCanMessage)
        {
            this.Logger = logger;
            this.PulseEventHandler = pulseEventHandler;
            var canEventHandler = readCanMessage.Start();
            canEventHandler.EventIsReached += this.CanEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        private ILogger Logger { get; }

        private IPulseEventHandler PulseEventHandler { get; }

        #endregion

        #region Private Methods

        private void CanEventHandler_EventIsReached(object sender, ReadCanMessageEventArgs e)
        {
            try
            {
                var id = e.Id;
                var data = e.Data.ToArray();

                // todo mb das nicht jedes mal im handler machen
                uint canPulseOffsset = 0x180;
                uint node = 4;
                var copIdPulseMinimum = node + canPulseOffsset;
                var copIdPulseMaximum = node + canPulseOffsset + 16;
                
                if (id >= copIdPulseMinimum && id < copIdPulseMaximum)
                {
                    var channel = id - copIdPulseMinimum;

                    var shiftData = new byte[4];
                    shiftData[0] = data[3]; // todo mb: beser unter schon ander rein?
                    shiftData[1] = data[2];
                    shiftData[2] = data[1];
                    shiftData[3] = data[0];

                    var pulseData = BitConverter.ToUInt32(shiftData, 0);
                    this.PulseEventHandler.OnReached(new PulseEventArgs(channel, pulseData));
                }
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