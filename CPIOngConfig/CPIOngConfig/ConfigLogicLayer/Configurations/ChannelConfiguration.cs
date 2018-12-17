using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigLogicLayer.Configurations
{
    using System.Runtime.CompilerServices;

    using ConfigLogicLayer.Contracts.Configurations;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    /// The channel configuration service.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.Configurations.IChannelConfiguration" />
    public class ChannelConfiguration : IChannelConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelConfiguration"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public ChannelConfiguration(ILogger logger, IWriteBasicCan writeBasicCan)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
        }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        public void Set(IReadOnlyList<ChannelConfigurationDto> channelConfigurationDto)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());


                foreach (var item in channelConfigurationDto)
                {
                    this.WriteBasicCan.
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
    }
}
