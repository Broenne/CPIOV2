namespace HardwareAbstaction.PCAN.PCANDll
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a PCAN device.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed. Suppression is OK here.")]
    public enum TpcanDevice : byte
    {
        /// <summary>
        /// Undefined, unknown or not selected PCAN device value.
        /// </summary>
        PCAN_NONE = 0,

        /// <summary>
        /// PCAN Non-Plug and Play devices. NOT USED WITHIN PCAN-Basic API.
        /// </summary>
        PCAN_PEAKCAN = 1,

        /// <summary>
        /// PCAN-ISA, PCAN-PC/104, and PCAN-PC/104-Plus.
        /// </summary>
        PCAN_ISA = 2,

        /// <summary>
        /// PCAN Dongle.
        /// </summary>
        PCAN_DNG = 3,

        /// <summary>
        /// PCAN-PCI, PCAN-cPCI, PCAN-miniPCI, and PCAN-PCI Express.
        /// </summary>
        PCAN_PCI = 4,

        /// <summary>
        /// PCAN-USB and PCAN-USB Pro.
        /// </summary>
        PCAN_USB = 5,

        /// <summary>
        /// PCAN-PC Card.
        /// </summary>
        PCAN_PCC = 6,

        /// <summary>
        /// PCAN Virtual hardware. NOT USED WITHIN PCAN-Basic API.
        /// </summary>
        PCAN_VIRTUAL = 7,

        /// <summary>
        /// PCAN Gateway devices.
        /// </summary>
        PCAN_LAN = 8
    }
}
