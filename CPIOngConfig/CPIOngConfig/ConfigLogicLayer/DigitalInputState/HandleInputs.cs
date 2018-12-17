namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Linq;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.InputBinary;
    using CPIOngConfig.Contracts.Pulse;
    using CPIOngConfig.InputBinary;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     Handle the hardware inputs.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.DigitalInputState.IHandleInputs" />
    public class HandleInputs : IHandleInputs
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="HandleInputs" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pulseEventHandler">The pulse event handler.</param>
        /// <param name="readCanMessage">The read can message.</param>
        /// <param name="inputBinaryEventHandler">The input binary event handler.</param>
        public HandleInputs(ILogger logger, IPulseEventHandler pulseEventHandler, IReadCanMessage readCanMessage, IInputBinaryEventHandler inputBinaryEventHandler)
        {
            this.Logger = logger;
            this.CanNodeIdToListen = 4;
            this.PulseEventHandler = pulseEventHandler;
            this.ReadCanMessage = readCanMessage;
            this.InputBinaryEventHandler = inputBinaryEventHandler;
        }

        #endregion

        #region Properties

        private uint CanNodeIdToListen { get; }

        private IInputBinaryEventHandler InputBinaryEventHandler { get; }

        private ILogger Logger { get; }

        private IPulseEventHandler PulseEventHandler { get; }

        private IReadCanMessage ReadCanMessage { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            try
            {
                // todo mb: mehrfach start verhindern?
                var canEventHandler = this.ReadCanMessage.Start();
                canEventHandler.EventIsReached += this.CanEventHandler_EventIsReached;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion

        #region Private Methods

        private void CanEventHandler_EventIsReached(object sender, ReadCanMessageEventArgs e)
        {
            try
            {
                var id = e.Id;
                var data = e.Data.ToArray();

                // die könnte man auslagern und dynamisch reinladen anhand eineer interface deklaration
                this.HandlePulseEvent(id, data);
                this.HandleBinaryInputState(id, data);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        private void HandleBinaryInputState(uint id, byte[] data)
        {
            try
            {
                if (this.CanNodeIdToListen == id)
                {
                    var inputBinbaryArgs = new InputBinaryEventArgs();

                    for (uint i = 0; i < 16; i++)
                    {
                        inputBinbaryArgs.Add(i, true);
                    }

                    this.InputBinaryEventHandler.OnReached(inputBinbaryArgs);
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        private void HandlePulseEvent(uint id, byte[] data)
        {
            try
            {
                // todo mb das nicht jedes mal im handler machen
                uint canPulseOffsset = 0x180;
                var node = this.CanNodeIdToListen;
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