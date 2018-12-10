namespace HardwareAbstaction.PCAN.PCANDll
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the type of PCAN (non plug and play) hardware to be initialized.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed. Suppression is OK here.")]
    public enum TPCANType : byte
    {
        /// <summary>
        /// PCAN-ISA 82C200.
        /// </summary>
        PCAN_TYPE_ISA = 0x01,

        /// <summary>
        /// PCAN-ISA SJA1000.
        /// </summary>
        PCAN_TYPE_ISA_SJA = 0x09,

        /// <summary>
        /// PHYTEC ISA.
        /// </summary>
        PCAN_TYPE_ISA_PHYTEC = 0x04,

        /// <summary>
        /// PCAN-Dongle 82C200.
        /// </summary>
        PCAN_TYPE_DNG = 0x02,

        /// <summary>
        /// PCAN-Dongle EPP 82C200.
        /// </summary>
        PCAN_TYPE_DNG_EPP = 0x03,

        /// <summary>
        /// PCAN-Dongle SJA1000.
        /// </summary>
        PCAN_TYPE_DNG_SJA = 0x05,

        /// <summary>
        /// PCAN-Dongle EPP SJA1000.
        /// </summary>
        PCAN_TYPE_DNG_SJA_EPP = 0x06,
    }
}
