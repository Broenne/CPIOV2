namespace HardwareAbstraction.Contracts.PCanDll
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a PCAN message.
    /// </summary>
    public struct TpcanMsg
    {
        /// <summary>
        /// 11/29-bit message identifier.
        /// </summary>
        public uint Id;

        /// <summary>
        /// Type of the message.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public TpcanMessageType Msgtype;

        /// <summary>
        /// Data Length Code of the message (0..8).
        /// </summary>
        public byte Len;

        /// <summary>
        /// Data of the message (DATA[0]..DATA[7]).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Data;
    }
}
