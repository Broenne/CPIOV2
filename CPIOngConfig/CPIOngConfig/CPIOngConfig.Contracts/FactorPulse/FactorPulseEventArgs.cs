namespace CPIOngConfig.Contracts.FactorPulse
{
    using System;

    public class FactorPulseEventArgs : EventArgs
    {
        public FactorPulseEventArgs(uint timeBase, double volumePerTimeSlot)
        {
            this.TimeBase = timeBase;
            this.VolumePerTimeSlot = volumePerTimeSlot;
        }


        public uint TimeBase { get; }

        public double VolumePerTimeSlot { get; }
    }
}