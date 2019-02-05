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

        /// <summary>
        /// Raises the <see cref="E:Reached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PulseEventArgs"/> instance containing the event data.</param>
        void OnReached(PulseEventArgs e);
    }
}