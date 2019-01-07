namespace ConfigLogicLayer.Contracts.Configurations
{
    using System;

    /// <summary>
    ///     The channel configuration response event argument.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ChannelConfigurationResponseEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelConfigurationResponseEventArgs"/> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public ChannelConfigurationResponseEventArgs(uint channel)
        {
            this.Channel = channel;
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

        #endregion
    }
}