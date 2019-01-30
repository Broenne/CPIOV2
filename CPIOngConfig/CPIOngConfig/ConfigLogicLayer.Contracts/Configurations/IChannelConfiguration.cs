namespace ConfigLogicLayer.Contracts.Configurations
{
    using System.Collections.Generic;

    /// <summary>
    /// The channel configuration.
    /// </summary>
    public interface IChannelConfiguration
    {
        /// <summary>
        /// Sets the specified channel configuration DTO.
        /// </summary>
        /// <param name="channelConfigurationDto">The channel configuration DTO.</param>
        void Set(IReadOnlyList<ChannelConfigurationDto> channelConfigurationDto);

        /// <summary>
        /// Triggers the state of to get.
        /// </summary>
        void TriggerToGetState();
    }
}