namespace Hal.PeakCan.PCANDll
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a PCAN parameter to be read or set.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "Reviewed. Suppression is OK here.")]
    public enum TpcanParameter : byte
    {
        /// <summary>
        /// PCAN-USB device number parameter.
        /// </summary>
        PCAN_DEVICE_NUMBER = 1,

        /// <summary>
        /// PCAN-PC Card 5-Volt power parameter.
        /// </summary>
        PCAN_5VOLTS_POWER = 2,

        /// <summary>
        /// PCAN receive event handler parameter.
        /// </summary>
        PCAN_RECEIVE_EVENT = 3,

        /// <summary>
        /// PCAN message filter parameter.
        /// </summary>
        PCAN_MESSAGE_FILTER = 4,

        /// <summary>
        /// PCAN-Basic API version parameter.
        /// </summary>
        PCAN_API_VERSION = 5,

        /// <summary>
        /// PCAN device channel version parameter.
        /// </summary>
        PCAN_CHANNEL_VERSION = 6,

        /// <summary>
        /// PCAN RESET-ON-BUSOFF parameter.
        /// </summary>
        PCAN_BUSOFF_AUTORESET = 7,

        /// <summary>
        /// PCAN Listen-Only parameter.
        /// </summary>
        PCAN_LISTEN_ONLY = 8,

        /// <summary>
        /// Directory path for log files.
        /// </summary>
        PCAN_LOG_LOCATION = 9,

        /// <summary>
        /// Debug-Log activation status.
        /// </summary>
        PCAN_LOG_STATUS = 10,

        /// <summary>
        /// Configuration of the debugged information (LOG_FUNCTION_***).
        /// </summary>
        PCAN_LOG_CONFIGURE = 11,

        /// <summary>
        /// Custom insertion of text into the log file.
        /// </summary>
        PCAN_LOG_TEXT = 12,

        /// <summary>
        /// Availability status of a PCAN-Channel.
        /// </summary>
        PCAN_CHANNEL_CONDITION = 13,

        /// <summary>
        /// PCAN hardware name parameter.
        /// </summary>
        PCAN_HARDWARE_NAME = 14,

        /// <summary>
        /// Message reception status of a PCAN-Channel.
        /// </summary>
        PCAN_RECEIVE_STATUS = 15,

        /// <summary>
        /// CAN-Controller number of a PCAN-Channel.
        /// </summary>
        PCAN_CONTROLLER_NUMBER = 16,

        /// <summary>
        /// Directory path for PCAN trace files.
        /// </summary>
        PCAN_TRACE_LOCATION = 17,

        /// <summary>
        /// CAN tracing activation status.
        /// </summary>
        PCAN_TRACE_STATUS = 18,

        /// <summary>
        /// Configuration of the maximum file size of a CAN trace.
        /// </summary> 
        PCAN_TRACE_SIZE = 19,

        /// <summary>
        /// Configuration of the trace file storing mode (TRACE_FILE_***).
        /// </summary>
        PCAN_TRACE_CONFIGURE = 20,

        /// <summary>
        /// Physical identification of a USB based PCAN-Channel by blinking its associated LED.
        /// </summary>
        PCAN_CHANNEL_IDENTIFYING = 21,

        /// <summary>
        /// Capabilities of a PCAN device (FEATURE_***).
        /// </summary>
        PCAN_CHANNEL_FEATURES = 22,

        /// <summary>
        /// Using of an existing bit rate (PCAN-View connected to a channel).
        /// </summary>
        PCAN_BITRATE_ADAPTING = 23,

        /// <summary>
        /// Configured bit rate as BTR0BTR1 value.
        /// </summary>
        PCAN_BITRATE_INFO = 24,

        /// <summary>
        /// Configured bit rate as TPCANBitrateFD string.
        /// </summary>
        PCAN_BITRATE_INFO_FD = 25,

        /// <summary>
        /// Configured nominal CAN Bus speed as Bits per seconds.
        /// </summary>
        PCAN_BUSSPEED_NOMINAL = 26,

        /// <summary>
        /// Configured CAN data speed as Bits per seconds.
        /// </summary>
        PCAN_BUSSPEED_DATA = 27,

        /// <summary>
        /// Remote address of a LAN channel as string in IPV4 format.
        /// </summary>
        PCAN_IP_ADDRESS = 28,

        /// <summary>
        /// Status of the Virtual PCAN-Gateway Service.
        /// </summary>
        PCAN_LAN_SERVICE_STATUS = 29,

        /// <summary>
        /// Status messages reception status within a PCAN-Channel.
        /// </summary>
        PCAN_ALLOW_STATUS_FRAMES = 30,

        /// <summary>
        /// RTR messages reception status within a PCAN-Channel.
        /// </summary>
        PCAN_ALLOW_RTR_FRAMES = 31,

        /// <summary>
        /// Error messages reception status within a PCAN-Channel.
        /// </summary>
        PCAN_ALLOW_ERROR_FRAMES = 32,

        /// <summary>
        /// Delay, in microseconds, between sending frames.
        /// </summary>
        PCAN_INTERFRAME_DELAY = 33,

        /// <summary>
        /// Filter over code and mask patterns for 11-Bit messages.
        /// </summary>
        PCAN_ACCEPTANCE_FILTER_11BIT = 34,

        /// <summary>
        /// Filter over code and mask patterns for 29-Bit messages.
        /// </summary>
        PCAN_ACCEPTANCE_FILTER_29BIT = 35,
    }
}
