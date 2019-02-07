using System;
using CPIOngConfig.Contracts.ConfigInputs;

namespace CPIOngConfig.ActiveSensor
{
    public interface IActiveSensorEventHandler
    {
        event EventHandler<Modi> EventIsReached;

        void OnReached(Modi e);
    }
}