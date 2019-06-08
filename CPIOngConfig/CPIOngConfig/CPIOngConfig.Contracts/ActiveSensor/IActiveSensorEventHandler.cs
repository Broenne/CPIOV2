using System;
using ConfigLogicLayer.DigitalInputState;
using CPIOngConfig.Contracts.ConfigInputs;

namespace CPIOngConfig.ActiveSensor
{
    public interface IActiveSensorEventHandler
    {
        event EventHandler</*Modi*/ActionHandleStates> EventIsReached;

        void OnReached(/*Modi*/ActionHandleStates e);
    }
}