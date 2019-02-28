namespace HardwareAbstraction.Contracts.PCanDll
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the type of a PCAN message.
    /// </summary>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed. Suppression is OK here.")]
    public enum TpcanMessageType : byte
    {
        /// <summary>
        /// The PCAN message is a CAN Standard Frame (11-bit identifier).
        /// </summary>
        PCAN_MESSAGE_STANDARD = 0x00,

        /// <summary>
        /// The PCAN message is a CAN Remote-Transfer-Request Frame.
        /// </summary>
        PCAN_MESSAGE_RTR = 0x01,

        /// <summary>
        /// The PCAN message is a CAN Extended Frame (29-bit identifier).
        /// </summary>
        PCAN_MESSAGE_EXTENDED = 0x02,

        /// <summary>
        /// The PCAN message represents a FD frame in terms of CIA Specs.
        /// </summary>
        PCAN_MESSAGE_FD = 0x04,

        /// <summary>
        /// The PCAN message represents a FD bit rate switch (CAN data at a higher bit rate).
        /// </summary>
        PCAN_MESSAGE_BRS = 0x08,

        /// <summary>
        /// The PCAN message represents a FD error state indicator(CAN FD transmitter was error active).
        /// </summary>
        PCAN_MESSAGE_ESI = 0x10,

        /// <summary>
        /// The PCAN message represents an error frame.
        /// </summary>
        PCAN_MESSAGE_ERRFRAME = 0x40,

        /// <summary>
        /// The PCAN message represents a PCAN status message.
        /// </summary>
        PCAN_MESSAGE_STATUS = 0x80,
    }
}