using System;

namespace Hal.PeakCan.Contracts.Basics
{
    public interface IReadCanMessageEvent
    {
        event EventHandler<ReadCanMessageEventArgs> EventIsReached;

        /// </summary>
        /// <param name="e">The <see cref="NodeEventArgs" /> instance containing the event data.</param>
        void OnReached(ReadCanMessageEventArgs e);
    }
}