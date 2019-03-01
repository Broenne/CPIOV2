namespace ConfigLogicLayer.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows;

    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Configurations;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The channel configuration service.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.Configurations.IChannelConfiguration" />
    public class ChannelConfiguration : IChannelConfiguration
    {
        private int waitForResponse = -1;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelConfiguration" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        /// <param name="channelConfigurationResponseEventHandler">The channel configuration response event handler.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        public ChannelConfiguration(ILogger logger, IWriteBasicCan writeBasicCan, IChannelConfigurationResponseEventHandler channelConfigurationResponseEventHandler, IGetActualNodeId getActualNodeId)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
            this.ChannelConfigurationResponseEventHandler = channelConfigurationResponseEventHandler;
            this.GetActualNodeId = getActualNodeId;
            this.ChannelConfigurationResponseEventHandler.EventIsReached += this.ChannelConfigurationResponseEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        private IChannelConfigurationResponseEventHandler ChannelConfigurationResponseEventHandler { get; }

        private IGetActualNodeId GetActualNodeId { get; }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the specified channel configuration DTO.
        /// </summary>
        /// <param name="channelConfigurationDto">The channel configuration DTO.</param>
        public void Set(IReadOnlyList<ChannelConfigurationDto> channelConfigurationDto)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                foreach (var item in channelConfigurationDto)
                {
                    this.waitForResponse = -1;
                    const byte WriteConfigByte = 0x03;
                    var data = new List<byte>();
                    data.Add(WriteConfigByte); // 0
                    data.Add(Convert.ToByte(item.Channel)); // 1
                    data.Add(Convert.ToByte(item.Modi)); // 2
                    data.Add(0x00); // 3
                    data.Add(Convert.ToByte(item.Modi)); // 4
                    data.Add(0x00); // 5
                    data.Add(0x00); // 6
                    data.Add(0x00); // 7

                    this.WriteBasicCan.WriteCan(0x00, data);

                    var i = 0;
                    while (this.waitForResponse != item.Channel)
                    {
                        Thread.Sleep(50);
                        ++i;
                        if (i > 50)
                        {
                            MessageBox.Show($"configuration Einfgänge fehlerhaft {item.Channel}");
                            goto Finish;
                        }
                    }
                }

                this.SafeChannelInputModi();

                Finish: ;
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

        /// <summary>
        ///     Triggers the state of to get.
        /// </summary>
        public void TriggerToGetState()
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                for (var i = 0; i < 16; i++)
                {
                    var data = new List<byte>();
                    data.Add(0x01);
                    data.Add(Convert.ToByte(i));

                    this.WriteBasicCan.WriteCan(this.GetActualNodeId.Get() + CanCommandConsts.TriggerGetInputConfigurationOffset, data);

                    this.waitForResponse = -1;

                    int maxCounter = 0;
                    while (this.waitForResponse != i)
                    {
                        Thread.Sleep(10);
                        if (maxCounter > 50)
                        {
                            throw new Exception($"No response on get configuration. Channel-Nr.: {i}");
                        }

                        ++maxCounter;
                    }
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

        #endregion

        #region Private Methods

        private void SafeChannelInputModi()
        {
            try
            {
                const byte WriteConfigByte = 0x04;
                var data = new List<byte>();
                data.Add(WriteConfigByte); // 0

                // save to eeprom
                this.WriteBasicCan.WriteCan(0x00, data);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        ///     Handles the EventIsReached event of the ChannelConfigurationResponseEventHandler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ChannelConfigurationResponseEventArgs" /> instance containing the event data.</param>
        private void ChannelConfigurationResponseEventHandler_EventIsReached(object sender, ChannelConfigurationResponseEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                this.waitForResponse = (int)e.Channel;
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