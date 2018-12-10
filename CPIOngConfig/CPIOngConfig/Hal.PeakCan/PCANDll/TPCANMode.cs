namespace HardwareAbstaction.PCAN.PCANDll
{
    using System.Diagnostics.CodeAnalysis;

    using HardwareAbstraction.Contracts.PCanDll;

    /// <summary>
    /// Represents a PCAN filter mode.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed. Suppression is OK here.")]
    public enum TpcanMode : byte
    {
        /// <summary>
        /// Mode is Standard (11-bit identifier).
        /// </summary>
        PCAN_MODE_STANDARD = TpcanMessageType.PCAN_MESSAGE_STANDARD,

        /// <summary>
        /// Mode is Extended (29-bit identifier).
        /// </summary>
        PCAN_MODE_EXTENDED = TpcanMessageType.PCAN_MESSAGE_EXTENDED,
    }
}
