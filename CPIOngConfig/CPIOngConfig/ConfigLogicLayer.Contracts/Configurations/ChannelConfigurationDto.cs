namespace ConfigLogicLayer.Contracts.Configurations
{
    using CPIOngConfig.Contracts.ConfigInputs;

    /// <summary>
    ///     The channel configuration DTO.
    /// </summary>
    public class ChannelConfigurationDto
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelConfigurationDto" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="modi">The modi info.</param>
        public ChannelConfigurationDto(uint channel, Modi modi)
        {
            this.Channel = channel;
            this.Modi = modi;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the channel.
        /// </summary>
        /// <value>
        ///     The channel.
        /// </value>
        public uint Channel { get; }

        /// <summary>
        ///     Gets the modi info.
        /// </summary>
        /// <value>
        ///     The modi info.
        /// </value>
        public Modi Modi { get; }

        #endregion
    }
}