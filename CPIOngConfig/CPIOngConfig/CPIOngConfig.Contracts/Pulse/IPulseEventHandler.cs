namespace CPIOngConfig.Contracts.Pulse
{
    using System;

    /// <summary>
    /// The pulse event handler.
    /// </summary>
    public interface IPulseEventHandler
    {
        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<PulseEventArgs> EventIsReached;

        void OnReached(PulseEventArgs e);
    }
}