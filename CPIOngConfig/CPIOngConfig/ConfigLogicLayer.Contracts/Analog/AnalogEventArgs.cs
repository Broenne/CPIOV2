namespace ConfigLogicLayer.Text
{
    using System;

    public class AnalogEventArgs : EventArgs
    {
        public AnalogEventArgs(uint channel, uint millivoltage)
        {
            this.Channel = channel;
            this.Millivoltage = millivoltage;
        }

        public uint Channel { get; }


        public uint Millivoltage { get; }
    }
}