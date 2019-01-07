namespace ConfigLogicLayer.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using ConfigLogicLayer.Contracts.Configurations;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The channel configuration service.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.Configurations.IChannelConfiguration" />
    public class ChannelConfiguration : IChannelConfiguration
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelConfiguration" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        /// <param name="channelConfigurationResponseEventHandler">The channel configuration response event handler.</param>
        public ChannelConfiguration(ILogger logger, IWriteBasicCan writeBasicCan, IChannelConfigurationResponseEventHandler channelConfigurationResponseEventHandler)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
            this.ChannelConfigurationResponseEventHandler = channelConfigurationResponseEventHandler;
            this.ChannelConfigurationResponseEventHandler.EventIsReached += this.ChannelConfigurationResponseEventHandler_EventIsReached;
        }

        private int waitForResponse; 
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

        #region Properties

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        private IChannelConfigurationResponseEventHandler ChannelConfigurationResponseEventHandler { get; }

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
                    data.Add(WriteConfigByte);
                    data.Add(Convert.ToByte(item.Channel));
                    data.Add(Convert.ToByte(item.Modi));

                    this.WriteBasicCan.WriteCan(0x00, data);

                    while (this.waitForResponse != item.Channel)
                    {
                        // todo mb: timeoput
                        Thread.Sleep(10);
                    }
                    // todo mb: auf antwort warten
                    //Thread.Sleep(50);
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
    }
}