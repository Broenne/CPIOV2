namespace ConfigLogicLayer.Contracts.Configurations
{
    using System;

    /// <summary>
    ///     The channel configuration response event handler.
    /// </summary>
    public interface IChannelConfigurationResponseEventHandler
    {
        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<ChannelConfigurationResponseEventArgs> EventIsReached;

        /// <summary>
        /// Raises the <see cref="E:Reached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ChannelConfigurationResponseEventArgs"/> instance containing the event data.</param>
        void OnReached(ChannelConfigurationResponseEventArgs e);
    }
}