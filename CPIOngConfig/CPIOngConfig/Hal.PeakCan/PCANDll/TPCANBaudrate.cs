namespace Hal.PeakCan.PCANDll
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a PCAN Baud rate register value.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed. Suppression is OK here.")]
    public enum TPCANBaudrate : ushort
    {
        /// <summary>
        /// 1 MBit/second.
        /// </summary>
        PCAN_BAUD_1M = 0x0014,

        /// <summary>
        /// 800 KBit/second.
        /// </summary>
        PCAN_BAUD_800K = 0x0016,

        /// <summary>
        /// 500 kBit/second.
        /// </summary>
        PCAN_BAUD_500K = 0x001C,

        /// <summary>
        /// 250 kBit/second.
        /// </summary>
        PCAN_BAUD_250K = 0x011C,

        /// <summary>
        /// 125 kBit/second.
        /// </summary>
        PCAN_BAUD_125K = 0x031C,

        /// <summary>
        /// 100 kBit/second.
        /// </summary>
        PCAN_BAUD_100K = 0x432F,

        /// <summary>
        /// 95,238 KBit/second.
        /// </summary>
        PCAN_BAUD_95K = 0xC34E,

        /// <summary>
        /// 83,333 KBit/second.
        /// </summary>
        PCAN_BAUD_83K = 0x852B,

        /// <summary>
        /// 50 kBit/second.
        /// </summary>
        PCAN_BAUD_50K = 0x472F,

        /// <summary>
        /// 47,619 KBit/second.
        /// </summary>
        PCAN_BAUD_47K = 0x1414,

        /// <summary>
        /// 33,333 KBit/second.
        /// </summary>
        PCAN_BAUD_33K = 0x8B2F,

        /// <summary>
        /// 20 kBit/second.
        /// </summary>
        PCAN_BAUD_20K = 0x532F,

        /// <summary>
        /// 10 kBit/second.
        /// </summary>
        PCAN_BAUD_10K = 0x672F,

        /// <summary>
        /// 5 kBit/second.
        /// </summary>
        PCAN_BAUD_5K = 0x7F7F,
    }
}