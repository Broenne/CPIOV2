using System;
using CPIOngConfig.Contracts.Pulse;

namespace CPIOngConfig.Pulse
{
    public interface IPulseEventHandler
    {
        event EventHandler<PulseEventArgs> EventIsReached;

        void OnReached(PulseEventArgs e);
    }
}