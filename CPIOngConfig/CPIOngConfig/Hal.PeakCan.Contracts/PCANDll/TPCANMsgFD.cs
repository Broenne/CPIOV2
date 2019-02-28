namespace HardwareAbstaction.PCAN.PCANDll
{
    using System.Runtime.InteropServices;

    using HardwareAbstraction.Contracts.PCanDll;

    /// <summary>
    /// Represents a PCAN message from a FD capable hardware.
    /// </summary>
    public struct TpcanMsgFd
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
        /// Data Length Code of the message (0..15).
        /// </summary>
        public byte Dlc;

        /// <summary>
        /// Data of the message (DATA[0]..DATA[63]).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] Data;
    }
}
