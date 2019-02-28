namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ConfigLogicLayer.Configurations;
    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Analog;
    using ConfigLogicLayer.Contracts.Configurations;
    using ConfigLogicLayer.Contracts.DigitalInputState;
    using ConfigLogicLayer.Text;

    using CPIOngConfig.ActiveSensor;
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.CanText;
    using CPIOngConfig.Contracts.ConfigInputs;
    using CPIOngConfig.Contracts.FlipFlop;
    using CPIOngConfig.Contracts.InputBinary;
    using CPIOngConfig.Contracts.Pulse;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     Handle the hardware inputs.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.DigitalInputState.IHandleInputs" />
    public class HandleInputs : IHandleInputs
    {
        private static readonly object LockObj = new object();

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleInputs" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pulseEventHandler">The pulse event handler.</param>
        /// <param name="readCanMessage">The read can message.</param>
        /// <param name="inputBinaryEventHandler">The input binary event handler.</param>
        /// <param name="aliveEventHandler">The alive event handler.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        /// <param name="channelConfigurationResponseEventHandler">The channel configuration response event handler.</param>
        /// <param name="canIsConnectedEventHandler">The can is connected event handler.</param>
        /// <param name="flipFlopEventHandler">The flip flop event handler.</param>
        /// <param name="activeSensorEventHandler">The active sensor event handler.</param>
        /// <param name="canTextEventHandler">The can text event handler.</param>
        /// <param name="textResponseEventHandler">The text response event handler.</param>
        /// <param name="analogEventHandler">The analog event handler.</param>
        public HandleInputs(ILogger logger, IPulseEventHandler pulseEventHandler, IReadCanMessage readCanMessage, IInputBinaryEventHandler inputBinaryEventHandler, IAliveEventHandler aliveEventHandler, IGetActualNodeId getActualNodeId, IChannelConfigurationResponseEventHandler channelConfigurationResponseEventHandler, ICanIsConnectedEventHandler canIsConnectedEventHandler, IFlipFlopEventHandler flipFlopEventHandler, IActiveSensorEventHandler activeSensorEventHandler, ICanTextEventHandler canTextEventHandler, ITextResponseEventHandler textResponseEventHandler, IAnalogEventHandler analogEventHandler)
        {
            this.Logger = logger;

            this.PulseEventHandler = pulseEventHandler;
            this.ReadCanMessage = readCanMessage;
            this.InputBinaryEventHandler = inputBinaryEventHandler;
            this.AliveEventHandler = aliveEventHandler;
            this.GetActualNodeId = getActualNodeId;
            this.ChannelConfigurationResponseEventHandler = channelConfigurationResponseEventHandler;
            this.CanIsConnectedEventHandler = canIsConnectedEventHandler;
            this.FlipFlopEventHandler = flipFlopEventHandler;
            this.ActiveSensorEventHandler = activeSensorEventHandler;
            this.CanTextEventHandler = canTextEventHandler;
            this.TextResponseEventHandler = textResponseEventHandler;
            this.AnalogEventHandler = analogEventHandler;
        }

        #endregion

        #region Properties

        private IActiveSensorEventHandler ActiveSensorEventHandler { get; }

        private IAliveEventHandler AliveEventHandler { get; }

        private IReadCanMessageEvent CanEventHandler { get; set; }

        private ICanIsConnectedEventHandler CanIsConnectedEventHandler { get; }

        private ICanTextEventHandler CanTextEventHandler { get; }

        private IChannelConfigurationResponseEventHandler ChannelConfigurationResponseEventHandler { get; }

        private IFlipFlopEventHandler FlipFlopEventHandler { get; }

        private IGetActualNodeId GetActualNodeId { get; }

        private IInputBinaryEventHandler InputBinaryEventHandler { get; }

        private ILogger Logger { get; }

        private IPulseEventHandler PulseEventHandler { get; }

        private IReadCanMessage ReadCanMessage { get; }

        private ITextResponseEventHandler TextResponseEventHandler { get; }

        private IAnalogEventHandler AnalogEventHandler { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            try
            {
                this.CanEventHandler = this.ReadCanMessage.Start();
                this.CanEventHandler.EventIsReached += this.CanEventHandler_EventIsReached;
                this.CanIsConnectedEventHandler.OnReached(true);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            try
            {
                this.CanIsConnectedEventHandler.OnReached(false);
                this.CanEventHandler.EventIsReached -= this.CanEventHandler_EventIsReached;
                this.ReadCanMessage.Stop();
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
                lock (LockObj)
                {
                    var id = e.Id;
                    var data = e.Data.ToArray();

                    // die könnte man auslagern und dynamisch reinladen anhand eineer interface deklaration
                    this.HandlePulseEvent(id, data);
                    this.HandleBinaryInputState(id, data);
                    this.HandleAlive(id, data);
                    this.HandleChannelConfigResponse(id, data);
                    this.HandleFlipFlopEvent(id, data);
                    this.ActiveSensorResponse(id, data);
                    this.TextResponse(id, data);
                    this.AnalogResponse(id, data);
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        private void AnalogResponse(uint id, byte[] data)
        {
            try
            {
                if (id.Equals(this.GetActualNodeId.Get() + CanCommandConsts.RequestAnalogValue))
                {
                    var channel = data[0];

                    if (channel == 5)
                    {
                        ;
                    }

                    var digits = BitConverter.ToUInt16(data, 1);
                    var milliVoltage = BitConverter.ToInt32(data, 3);

                    this.AnalogEventHandler.OnReached(new AnalogEventArgs(channel, digits, Convert.ToUInt32(milliVoltage)));
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TextResponse(uint id, byte[] data)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                if (id == CanCommandConsts.Text + this.GetActualNodeId.Get() && data[2].Equals(0xFF))
                {
                    this.TextResponseEventHandler.OnReached(new List<byte>(data).AsReadOnly());
                }
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

        private void ActiveSensorResponse(uint id, byte[] data)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                if (id == CanCommandConsts.SensorModiResponse + this.GetActualNodeId.Get() && data[0] == 0x02)
                {
                    var modi = (Modi)data[1];
                    this.ActiveSensorEventHandler.OnReached(modi);
                }
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

        private void HandleFlipFlopEvent(uint id, byte[] data)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                if (id == 0x170 + this.GetActualNodeId.Get())
                {
                    this.FlipFlopEventHandler.OnReached(new FlipFlopEventArgs(data));
                }
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

        private void HandleChannelConfigResponse(uint id, byte[] data)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                if (id == CanCommandConsts.SensorModiResponse + this.GetActualNodeId.Get() && data[0] == 0x01)
                {
                    this.ChannelConfigurationResponseEventHandler.OnReached(new ChannelConfigurationResponseEventArgs(data[1], (Modi)data[2]));
                }
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

        private void HandleAlive(uint id, byte[] data)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                if (id == CanCommandConsts.AliveOffset + this.GetActualNodeId.Get())
                {
                    var version = new Version(data[0], data[1], data[2]);
                    this.AliveEventHandler.OnReached(new AliveEventArgs(version, data[3], data[4], data[5], data[6]));

                    this.CanTextEventHandler.OnReached(data[7]);
                }
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

        private void HandleBinaryInputState(uint id, byte[] data)
        {
            try
            {
                if (this.GetActualNodeId.Get() == id)
                {
                    var inputBinbaryArgs = new InputBinaryEventArgs();

                    for (byte i = 0; i < 16; i++)
                    {
                        var res = ((data[i / 8] >> (i % 8)) & 0x01) == 1;
                        inputBinbaryArgs.Add(i, res);
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
                uint canPulseOffsset = CanCommandConsts.PulseId;
                var node = (int)this.GetActualNodeId.Get();

                // es kann max 3 geben
                if (id >= node && id <= (node + 3))
                {
                    var shiftData = new byte[4];
                    shiftData[0] = data[3];
                    shiftData[1] = data[2];
                    shiftData[2] = data[1];
                    shiftData[3] = data[0];

                    var channel = data[6];
                    var checkSum = data[7];

                    var pulseData = BitConverter.ToUInt32(shiftData, 0);
                    this.PulseEventHandler.OnReached(new PulseEventArgs(channel, pulseData, checkSum));
                }



                //var copIdPulseMinimum = node + canPulseOffsset;
                //var copIdPulseMaximum = node + canPulseOffsset + 16;

                //if (id >= copIdPulseMinimum && id < copIdPulseMaximum)
                //{
                //    var channel = id - copIdPulseMinimum;

                //    var shiftData = new byte[4];
                //    shiftData[0] = data[3];
                //    shiftData[1] = data[2];
                //    shiftData[2] = data[1];
                //    shiftData[3] = data[0];

                //    var checkSum = data[7];

                //    var pulseData = BitConverter.ToUInt32(shiftData, 0);
                //    this.PulseEventHandler.OnReached(new PulseEventArgs(channel, pulseData, checkSum));
                //}
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