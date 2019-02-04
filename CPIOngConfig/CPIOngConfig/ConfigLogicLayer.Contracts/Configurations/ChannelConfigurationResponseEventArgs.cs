namespace ConfigLogicLayer.Contracts.Configurations
{
    using System;

    using CPIOngConfig.Contracts.ConfigInputs;

    /// <summary>
    ///     The channel configuration response event argument.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ChannelConfigurationResponseEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelConfigurationResponseEventArgs" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="modi">The modi input.</param>
        public ChannelConfigurationResponseEventArgs(uint channel, Modi modi)
        {
            this.Channel = channel;
            this.Modi = modi;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public uint Channel { get; }

        /// <summary>
        /// Gets the modi info.
        /// </summary>
        /// <value>
        /// The modi info.
        /// </value>
        public Modi Modi { get; }

        #endregion
    }
}