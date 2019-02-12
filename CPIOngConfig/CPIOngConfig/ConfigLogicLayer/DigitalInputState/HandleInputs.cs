﻿namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Linq;

    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Configurations;
    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.ActiveSensor;
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.Contracts.Alive;
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
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="HandleInputs" /> class.
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
        public HandleInputs(ILogger logger, IPulseEventHandler pulseEventHandler, IReadCanMessage readCanMessage, IInputBinaryEventHandler inputBinaryEventHandler, IAliveEventHandler aliveEventHandler, IGetActualNodeId getActualNodeId, IChannelConfigurationResponseEventHandler channelConfigurationResponseEventHandler, ICanIsConnectedEventHandler canIsConnectedEventHandler, IFlipFlopEventHandler flipFlopEventHandler, IActiveSensorEventHandler activeSensorEventHandler)
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
        }

        #endregion

        #region Properties

        private IActiveSensorEventHandler ActiveSensorEventHandler { get; }

        private IAliveEventHandler AliveEventHandler { get; }

        private IReadCanMessageEvent CanEventHandler { get; set; }

        private ICanIsConnectedEventHandler CanIsConnectedEventHandler { get; }

        private IChannelConfigurationResponseEventHandler ChannelConfigurationResponseEventHandler { get; }

        private IFlipFlopEventHandler FlipFlopEventHandler { get; }

        private IGetActualNodeId GetActualNodeId { get; }

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
                this.CanEventHandler = this.ReadCanMessage.Start();
                this.CanEventHandler.EventIsReached += this.CanEventHandler_EventIsReached;
                this.CanIsConnectedEventHandler.OnReached(true); // todo mb: was ist wenn es schief geht
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                // todo mb: mehrfach start verhindern?
                this.ReadCanMessage.Stop();
                this.CanEventHandler.EventIsReached -= this.CanEventHandler_EventIsReached;
                this.CanIsConnectedEventHandler.OnReached(false); // todo mb: was ist wenn es schief geht
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
                this.HandleAlive(id, data);
                this.HandleChannelConfigResponse(id, data);
                this.HandleFlipFlopEvent(id, data);
                this.ActiveSensorResponse(id, data);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
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
                uint canPulseOffsset = 0x180;
                var node = this.GetActualNodeId.Get();
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