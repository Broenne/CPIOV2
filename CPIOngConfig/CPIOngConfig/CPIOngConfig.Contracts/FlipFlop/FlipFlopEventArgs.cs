namespace CPIOngConfig.Contracts.FlipFlop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FlipFlopEventArgs : EventArgs
    {
        public FlipFlopEventArgs(IEnumerable<byte> rawData)
        {
            this.RawData = rawData.ToList();
        }


        public IReadOnlyList<byte> RawData { get; }
    }
}