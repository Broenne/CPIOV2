using System;
using System.Collections.Generic;
using System.Text;

namespace Hal.PeakCan.Contracts.Basics
{
    public class ReadCanMessageEventArgs : EventArgs
    {
        public ReadCanMessageEventArgs(uint id, IReadOnlyList<byte> data)
        {
            Id = id;
            Data = data;
        }


        public uint Id { get; }

        public IReadOnlyList<byte> Data { get; }

    }
}
