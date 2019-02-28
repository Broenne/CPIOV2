namespace HardwareAbstaction.PCAN.PCANDll
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a PCAN status/error code.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed. Suppression is OK here.")]
    [Flags]
    public enum TPCANStatus : uint
    {
        /// <summary>
        /// No error on.
        /// </summary>
        PCAN_ERROR_OK = 0x00000,

        /// <summary>
        /// Transmit buffer in CAN controller is full.
        /// </summary>
        PCAN_ERROR_XMTFULL = 0x00001,

        /// <summary>
        /// CAN controller was read too late.        
        /// </summary>
        PCAN_ERROR_OVERRUN = 0x00002,

        /// <summary>
        /// Bus error: an error counter reached the 'light' limit.
        /// </summary>
        PCAN_ERROR_BUSLIGHT = 0x00004,

        /// <summary>
        /// Bus error: an error counter reached the 'heavy' limit.
        /// </summary>
        PCAN_ERROR_BUSHEAVY = 0x00008,

        /// <summary>
        /// Bus error: an error counter reached the 'warning' limit.
        /// </summary>
        PCAN_ERROR_BUSWARNING = PCAN_ERROR_BUSHEAVY,

        /// <summary>
        /// Bus error: the CAN controller is error passive.
        /// </summary>
        PCAN_ERROR_BUSPASSIVE = 0x40000,

        /// <summary>
        /// Bus error: the CAN controller is in bus-off state.
        /// </summary>
        PCAN_ERROR_BUSOFF = 0x00010,

        /// <summary>
        /// Mask for all bus errors.
        /// </summary>
        PCAN_ERROR_ANYBUSERR = (PCAN_ERROR_BUSWARNING | PCAN_ERROR_BUSLIGHT | PCAN_ERROR_BUSHEAVY | PCAN_ERROR_BUSOFF | PCAN_ERROR_BUSPASSIVE),

        /// <summary>
        /// Receive queue is empty.
        /// </summary>
        PCAN_ERROR_QRCVEMPTY = 0x00020,

        /// <summary>
        /// Receive queue was read too late.
        /// </summary>
        PCAN_ERROR_QOVERRUN = 0x00040,

        /// <summary>
        /// Transmit queue is full.
        /// </summary>
        PCAN_ERROR_QXMTFULL = 0x00080,

        /// <summary>
        /// Test of the CAN controller hardware registers failed (no hardware found).
        /// </summary>
        PCAN_ERROR_REGTEST = 0x00100,

        /// <summary>
        /// Driver not loaded.
        /// </summary>
        PCAN_ERROR_NODRIVER = 0x00200,

        /// <summary>
        /// Hardware already in use by a Net.
        /// </summary>
        PCAN_ERROR_HWINUSE = 0x00400,

        /// <summary>
        /// A Client is already connected to the Net.
        /// </summary>
        PCAN_ERROR_NETINUSE = 0x00800,

        /// <summary>
        /// Hardware handle is invalid.
        /// </summary>
        PCAN_ERROR_ILLHW = 0x01400,

        /// <summary>
        /// Net handle is invalid.
        /// </summary>
        PCAN_ERROR_ILLNET = 0x01800,

        /// <summary>
        /// Client handle is invalid.
        /// </summary>
        PCAN_ERROR_ILLCLIENT = 0x01C00,

        /// <summary>
        /// Mask for all handle errors.
        /// </summary>
        PCAN_ERROR_ILLHANDLE = (PCAN_ERROR_ILLHW | PCAN_ERROR_ILLNET | PCAN_ERROR_ILLCLIENT),

        /// <summary>
        /// Resource (FIFO, Client, timeout) cannot be created.
        /// </summary>
        PCAN_ERROR_RESOURCE = 0x02000,

        /// <summary>
        /// Invalid parameter.
        /// </summary>
        PCAN_ERROR_ILLPARAMTYPE = 0x04000,

        /// <summary>
        /// Invalid parameter value.
        /// </summary>
        PCAN_ERROR_ILLPARAMVAL = 0x08000,

        /// <summary>
        /// Unknown error.
        /// </summary>
        PCAN_ERROR_UNKNOWN = 0x10000,

        /// <summary>
        /// Invalid data, function, or action.
        /// </summary>
        PCAN_ERROR_ILLDATA = 0x20000,

        /// <summary>
        /// An operation was successfully carried out, however, irregularities were registered.
        /// </summary>
        PCAN_ERROR_CAUTION = 0x2000000,

        /// <summary>
        /// Channel is not initialized
        /// <remarks>Value was changed from 0x40000 to 0x4000000</remarks>.
        /// </summary>
        PCAN_ERROR_INITIALIZE = 0x4000000,

        /// <summary>
        /// Invalid operation
        /// <remarks>Value was changed from 0x80000 to 0x8000000</remarks>.
        /// </summary>
        PCAN_ERROR_ILLOPERATION = 0x8000000,
    }
}
