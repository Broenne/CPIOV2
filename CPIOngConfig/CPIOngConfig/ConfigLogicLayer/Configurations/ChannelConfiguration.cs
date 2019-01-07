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
        ///     Initializes a new instance of the <see cref="ChannelConfiguration" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public ChannelConfiguration(ILogger logger, IWriteBasicCan writeBasicCan)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
        }

        #endregion

        #region Properties

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
                    const byte WriteConfigByte = 0x03;
                    var data = new List<byte>();
                    data.Add(WriteConfigByte);
                    data.Add(Convert.ToByte(item.Channel));
                    data.Add(Convert.ToByte(item.Modi));

                    this.WriteBasicCan.WriteCan(0x00, data);

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