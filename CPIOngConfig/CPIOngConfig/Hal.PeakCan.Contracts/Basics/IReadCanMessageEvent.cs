namespace Hal.PeakCan.Contracts.Basics
{
    using System;

    /// <summary>
    /// The read can message event.
    /// </summary>
    public interface IReadCanMessageEvent
    {
        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<ReadCanMessageEventArgs> EventIsReached;

        /// <summary>
        /// Raises the <see cref="E:Reached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ReadCanMessageEventArgs"/> instance containing the event data.</param>
        void OnReached(ReadCanMessageEventArgs e);
    }
}