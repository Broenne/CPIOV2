using System;

namespace ConfigLogicLayer.Text
{
    public interface IAnalogEventHandler
    {
        event EventHandler<AnalogEventArgs> EventIsReached;

        void OnReached(AnalogEventArgs e);
    }
}