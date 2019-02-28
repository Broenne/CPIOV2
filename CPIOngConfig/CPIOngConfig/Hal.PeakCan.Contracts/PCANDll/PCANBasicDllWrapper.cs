namespace Hal.PeakCan.PCANDll
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Text;

    using HardwareAbstaction.PCAN.PCANDll;

    using HardwareAbstraction.Contracts.PCanDll;

    /// <summary>
    ///     The PCAN basic dll wrapper.
    /// </summary>
    [SuppressMessage(
        "StyleCop.CSharp.NamingRules",
        "SA1310:FieldNamesMustNotContainUnderscore",
        Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage(
        "StyleCop.CSharp.NamingRules",
        "SA1306:FieldNamesMustBeginWithLowerCaseLetter",
        Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage(
        "StyleCop.CSharp.ReadabilityRules",
        "SA1121:UseBuiltInTypeAlias",
        Justification = "Reviewed. Suppression is OK here.")]
    public static class PcanBasicDllWrapper
    {
        #region Constants

        /// <summary>
        ///     Device supports a delay between sending frames (FPGA based USB devices).
        /// </summary>
        public const int FEATURE_DELAY_CAPABLE = 0x02;

        /// <summary>
        ///     Device supports flexible data-rate (CAN-FD).
        /// </summary>
        public const int FEATURE_FD_CAPABLE = 0x01;

        /// <summary>
        ///     Logs all possible information within the PCAN-Basic API functions.
        /// </summary>
        public const int LOG_FUNCTION_ALL = 0xFFFF;

        /// <summary>
        ///     Logs system exceptions / errors.
        /// </summary>
        public const int LOG_FUNCTION_DEFAULT = 0x00;

        /// <summary>
        ///     Logs the entries to the PCAN-Basic API functions.
        /// </summary>
        public const int LOG_FUNCTION_ENTRY = 0x01;

        /// <summary>
        ///     Logs the exits from the PCAN-Basic API functions.
        /// </summary>
        public const int LOG_FUNCTION_LEAVE = 0x04;

        /// <summary>
        ///     Logs the parameters passed to the PCAN-Basic API functions.
        /// </summary>
        public const int LOG_FUNCTION_PARAMETERS = 0x02;

        /// <summary>
        ///     Logs the CAN messages received within the CAN_Read function.
        /// </summary>
        public const int LOG_FUNCTION_READ = 0x10;

        /// <summary>
        ///     Logs the CAN messages passed to the CAN_Write function.
        /// </summary>
        public const int LOG_FUNCTION_WRITE = 0x08;

        /// <summary>
        ///     Clock frequency in HERZ (80000000, 60000000, 40000000, 30000000, 24000000, 20000000).
        /// </summary>
        [SuppressMessage(
            "StyleCop.CSharp.DocumentationRules",
            "SA1631:DocumentationMustMeetCharacterPercentage",
            Justification = "Reviewed. Suppression is OK here.")]
        public const string PCAN_BR_CLOCK = "f_clock";

        /// <summary>
        ///     Clock frequency in MEGAHERZ (80, 60, 40, 30, 24, 20).
        /// </summary>
        public const string PCAN_BR_CLOCK_MHZ = "f_clock_mhz";

        /// <summary>
        ///     Clock PRESCALER for high speed data time quantum.
        /// </summary>
        public const string PCAN_BR_DATA_BRP = "data_brp";

        /// <summary>
        ///     Secondary sample point delay for high speed data bit rate in cycles.
        /// </summary>
        public const string PCAN_BR_DATA_SAMPLE = "data_ssp_offset";

        /// <summary>
        ///     Synchronization Jump Width for high speed data bit rate in time quanta.
        /// </summary>
        public const string PCAN_BR_DATA_SJW = "data_sjw";

        /// <summary>
        ///     TSEG1 segment for fast data bit rate in time quanta.
        /// </summary>
        public const string PCAN_BR_DATA_TSEG1 = "data_tseg1";

        /// <summary>
        ///     TSEG2 segment for fast data bit rate in time quanta.
        /// </summary>
        public const string PCAN_BR_DATA_TSEG2 = "data_tseg2";

        /// <summary>
        ///     Clock PRESCALER for nominal time quantum.
        /// </summary>
        public const string PCAN_BR_NOM_BRP = "nom_brp";

        /// <summary>
        ///     Sample point for nominal bit rate.
        /// </summary>
        public const string PCAN_BR_NOM_SAMPLE = "nom_sam";

        /// <summary>
        ///     Synchronization Jump Width for nominal bit rate in time quanta.
        /// </summary>
        public const string PCAN_BR_NOM_SJW = "nom_sjw";

        /// <summary>
        ///     TSEG1 segment for nominal bit rate in time quanta.
        /// </summary>
        public const string PCAN_BR_NOM_TSEG1 = "nom_tseg1";

        /// <summary>
        ///     TSEG2 segment for nominal bit rate in time quanta.
        /// </summary>
        public const string PCAN_BR_NOM_TSEG2 = "nom_tseg2";

        /// <summary>
        ///     The PCAN-Channel handle is available to be connected (Plug and Play Hardware: it means furthermore that the
        ///     hardware is plugged-in).
        /// </summary>
        public const int PCAN_CHANNEL_AVAILABLE = 1;

        /// <summary>
        ///     The PCAN-Channel handle is valid, and is already being used.
        /// </summary>
        public const int PCAN_CHANNEL_OCCUPIED = 2;

        /// <summary>
        ///     The PCAN-Channel handle is already being used by a PCAN-View application, but is available to connect.
        /// </summary>
        public const int PCAN_CHANNEL_PCANVIEW = PCAN_CHANNEL_AVAILABLE | PCAN_CHANNEL_OCCUPIED;

        /// <summary>
        ///     The PCAN-Channel handle is illegal, or its associated hardware is not available.
        /// </summary>
        public const int PCAN_CHANNEL_UNAVAILABLE = 0;

        /// <summary>
        ///     PPCAN-Dongle/LPT interface, channel 1.
        /// </summary>
        public const ushort PCAN_DNGBUS1 = 0x31;

        /// <summary>
        ///     The PCAN filter is closed. No messages will be received.
        /// </summary>
        public const int PCAN_FILTER_CLOSE = 0;

        /// <summary>
        ///     The PCAN filter is custom configured. Only registered
        ///     messages will be received.
        /// </summary>
        public const int PCAN_FILTER_CUSTOM = 2;

        /// <summary>
        ///     The PCAN filter is fully opened. All messages will be received.
        /// </summary>
        public const int PCAN_FILTER_OPEN = 1;

        /// <summary>
        ///     PCAN-ISA interface, channel 1.
        /// </summary>
        public const ushort PCAN_ISABUS1 = 0x21;

        /// <summary>
        ///     PCAN-ISA interface, channel 2.
        /// </summary>
        public const ushort PCAN_ISABUS2 = 0x22;

        /// <summary>
        ///     PCAN-ISA interface, channel 3.
        /// </summary>
        public const ushort PCAN_ISABUS3 = 0x23;

        /// <summary>
        ///     PCAN-ISA interface, channel 4.
        /// </summary>
        public const ushort PCAN_ISABUS4 = 0x24;

        /// <summary>
        ///     PCAN-ISA interface, channel 5.
        /// </summary>
        public const ushort PCAN_ISABUS5 = 0x25;

        /// <summary>
        ///     PCAN-ISA interface, channel 6.
        /// </summary>
        public const ushort PCAN_ISABUS6 = 0x26;

        /// <summary>
        ///     PCAN-ISA interface, channel 7.
        /// </summary>
        public const ushort PCAN_ISABUS7 = 0x27;

        /// <summary>
        ///     PCAN-ISA interface, channel 8.
        /// </summary>
        public const ushort PCAN_ISABUS8 = 0x28;

        /// <summary>
        ///     PCAN-LAN interface, channel 1.
        /// </summary>
        public const ushort PCAN_LANBUS1 = 0x801;

        /// <summary>
        ///     PCAN-LAN interface, channel 10.
        /// </summary>
        public const ushort PCAN_LANBUS10 = 0x80A;

        /// <summary>
        ///     PCAN-LAN interface, channel 11.
        /// </summary>
        public const ushort PCAN_LANBUS11 = 0x80B;

        /// <summary>
        ///     PCAN-LAN interface, channel 12.
        /// </summary>
        public const ushort PCAN_LANBUS12 = 0x80C;

        /// <summary>
        ///     PCAN-LAN interface, channel 13.
        /// </summary>
        public const ushort PCAN_LANBUS13 = 0x80D;

        /// <summary>
        ///     PCAN-LAN interface, channel 14.
        /// </summary>
        public const ushort PCAN_LANBUS14 = 0x80E;

        /// <summary>
        ///     PCAN-LAN interface, channel 15.
        /// </summary>
        public const ushort PCAN_LANBUS15 = 0x80F;

        /// <summary>
        ///     PCAN-LAN interface, channel 16.
        /// </summary>
        public const ushort PCAN_LANBUS16 = 0x810;

        /// <summary>
        ///     PCAN-LAN interface, channel 2.
        /// </summary>
        public const ushort PCAN_LANBUS2 = 0x802;

        /// <summary>
        ///     PCAN-LAN interface, channel 3.
        /// </summary>
        public const ushort PCAN_LANBUS3 = 0x803;

        /// <summary>
        ///     PCAN-LAN interface, channel 4.
        /// </summary>
        public const ushort PCAN_LANBUS4 = 0x804;

        /// <summary>
        ///     PCAN-LAN interface, channel 5.
        /// </summary>
        public const ushort PCAN_LANBUS5 = 0x805;

        /// <summary>
        ///     PCAN-LAN interface, channel 6.
        /// </summary>
        public const ushort PCAN_LANBUS6 = 0x806;

        /// <summary>
        ///     PCAN-LAN interface, channel 7.
        /// </summary>
        public const ushort PCAN_LANBUS7 = 0x807;

        /// <summary>
        ///     PCAN-LAN interface, channel 8.
        /// </summary>
        public const ushort PCAN_LANBUS8 = 0x808;

        /// <summary>
        ///     PCAN-LAN interface, channel 9.
        /// </summary>
        public const ushort PCAN_LANBUS9 = 0x809;

        /// <summary>
        ///     Undefined/default value for a PCAN bus.
        /// </summary>
        public const ushort PCAN_NONEBUS = 0x00;

        /// <summary>
        ///     The PCAN parameter is not set (inactive).
        /// </summary>
        public const int PCAN_PARAMETER_OFF = 0;

        /// <summary>
        ///     The PCAN parameter is set (active).
        /// </summary>
        public const int PCAN_PARAMETER_ON = 1;

        /// <summary>
        ///     PCAN-PC Card interface, channel 1.
        /// </summary>
        public const ushort PCAN_PCCBUS1 = 0x61;

        /// <summary>
        ///     PCAN-PC Card interface, channel 2.
        /// </summary>
        public const ushort PCAN_PCCBUS2 = 0x62;

        /// <summary>
        ///     PCAN-PCI interface, channel 1.
        /// </summary>
        public const ushort PCAN_PCIBUS1 = 0x41;

        /// <summary>
        ///     PCAN-PCI interface, channel 10.
        /// </summary>
        public const ushort PCAN_PCIBUS10 = 0x40A;

        /// <summary>
        ///     PCAN-PCI interface, channel 11.
        /// </summary>
        public const ushort PCAN_PCIBUS11 = 0x40B;

        /// <summary>
        ///     PCAN-PCI interface, channel 12.
        /// </summary>
        public const ushort PCAN_PCIBUS12 = 0x40C;

        /// <summary>
        ///     PCAN-PCI interface, channel 13.
        /// </summary>
        public const ushort PCAN_PCIBUS13 = 0x40D;

        /// <summary>
        ///     PCAN-PCI interface, channel 14.
        /// </summary>
        public const ushort PCAN_PCIBUS14 = 0x40E;

        /// <summary>
        ///     PCAN-PCI interface, channel 15.
        /// </summary>
        public const ushort PCAN_PCIBUS15 = 0x40F;

        /// <summary>
        ///     PCAN-PCI interface, channel 16.
        /// </summary>
        public const ushort PCAN_PCIBUS16 = 0x410;

        /// <summary>
        ///     PCAN-PCI interface, channel 2.
        /// </summary>
        public const ushort PCAN_PCIBUS2 = 0x42;

        /// <summary>
        ///     PCAN-PCI interface, channel 3.
        /// </summary>
        public const ushort PCAN_PCIBUS3 = 0x43;

        /// <summary>
        ///     PCAN-PCI interface, channel 4.
        /// </summary>
        public const ushort PCAN_PCIBUS4 = 0x44;

        /// <summary>
        ///     PCAN-PCI interface, channel 5.
        /// </summary>
        public const ushort PCAN_PCIBUS5 = 0x45;

        /// <summary>
        ///     PCAN-PCI interface, channel 6.
        /// </summary>
        public const ushort PCAN_PCIBUS6 = 0x46;

        /// <summary>
        ///     PCAN-PCI interface, channel 7.
        /// </summary>
        public const ushort PCAN_PCIBUS7 = 0x47;

        /// <summary>
        ///     PCAN-PCI interface, channel 8.
        /// </summary>
        public const ushort PCAN_PCIBUS8 = 0x48;

        /// <summary>
        ///     PCAN-PCI interface, channel 9.
        /// </summary>
        public const ushort PCAN_PCIBUS9 = 0x409;

        /// <summary>
        ///     PCAN-USB interface, channel 1.
        /// </summary>
        public const ushort PCAN_USBBUS1 = 0x51;

        /// <summary>
        ///     PCAN-USB interface, channel 10.
        /// </summary>
        public const ushort PCAN_USBBUS10 = 0x50A;

        /// <summary>
        ///     PCAN-USB interface, channel 11.
        /// </summary>
        public const ushort PCAN_USBBUS11 = 0x50B;

        /// <summary>
        ///     PCAN-USB interface, channel 12.
        /// </summary>
        public const ushort PCAN_USBBUS12 = 0x50C;

        /// <summary>
        ///     PCAN-USB interface, channel 13.
        /// </summary>
        public const ushort PCAN_USBBUS13 = 0x50D;

        /// <summary>
        ///     PCAN-USB interface, channel 14.
        /// </summary>
        public const ushort PCAN_USBBUS14 = 0x50E;

        /// <summary>
        ///     PCAN-USB interface, channel 15.
        /// </summary>
        public const ushort PCAN_USBBUS15 = 0x50F;

        /// <summary>
        ///     PCAN-USB interface, channel 16.
        /// </summary>
        public const ushort PCAN_USBBUS16 = 0x510;

        /// <summary>
        ///     PCAN-USB interface, channel 2.
        /// </summary>
        public const ushort PCAN_USBBUS2 = 0x52;

        /// <summary>
        ///     PCAN-USB interface, channel 3.
        /// </summary>
        public const ushort PCAN_USBBUS3 = 0x53;

        /// <summary>
        ///     PCAN-USB interface, channel 4.
        /// </summary>
        public const ushort PCAN_USBBUS4 = 0x54;

        /// <summary>
        ///     PCAN-USB interface, channel 5.
        /// </summary>
        public const ushort PCAN_USBBUS5 = 0x55;

        /// <summary>
        ///     PCAN-USB interface, channel 6.
        /// </summary>
        public const ushort PCAN_USBBUS6 = 0x56;

        /// <summary>
        ///     PCAN-USB interface, channel 7.
        /// </summary>
        public const ushort PCAN_USBBUS7 = 0x57;

        /// <summary>
        ///     PCAN-USB interface, channel 8.
        /// </summary>
        public const ushort PCAN_USBBUS8 = 0x58;

        /// <summary>
        ///     PCAN-USB interface, channel 9.
        /// </summary>
        public const ushort PCAN_USBBUS9 = 0x509;

        /// <summary>
        ///     The service is running.
        /// </summary>
        public const int SERVICE_STATUS_RUNNING = 0x04;

        /// <summary>
        ///     The service is not running.
        /// </summary>
        public const int SERVICE_STATUS_STOPPED = 0x01;

        /// <summary>
        ///     Includes the date into the name of the trace file.
        /// </summary>
        public const int TRACE_FILE_DATE = 0x02;

        /// <summary>
        ///     Causes the overwriting of available traces (same name).
        /// </summary>
        public const int TRACE_FILE_OVERWRITE = 0x80;

        /// <summary>
        ///     Traced data is distributed in several files with size PAN_TRACE_SIZE.
        /// </summary>
        public const int TRACE_FILE_SEGMENTED = 0x01;

        /// <summary>
        ///     A single file is written until it size reaches PAN_TRACE_SIZE.
        /// </summary>
        public const int TRACE_FILE_SINGLE = 0x00;

        /// <summary>
        ///     Includes the start time into the name of the trace file.
        /// </summary>
        public const int TRACE_FILE_TIME = 0x04;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Configures the reception filter.
        /// </summary>
        /// <remarks>
        ///     The message filter will be expanded with every call to
        ///     this function. If it is desired to reset the filter, please use
        ///     the 'SetValue' function.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="FromID">The lowest CAN ID to be received.</param>
        /// <param name="ToID">The highest CAN ID to be received.</param>
        /// <param name="Mode">
        ///     Message type, Standard (11-bit identifier) or
        ///     Extended (29-bit identifier).
        /// </param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_FilterMessages")]
        public static extern TPCANStatus FilterMessages(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            uint FromID,
            uint ToID,
            [MarshalAs(UnmanagedType.U1)] TpcanMode Mode);

        /// <summary>
        ///     Returns a descriptive text of a given TPCANStatus error
        ///     code, in any desired language.
        /// </summary>
        /// <remarks>
        ///     The current languages available for translation are:
        ///     Neutral (0x00), German (0x07), English (0x09), Spanish (0x0A),
        ///     Italian (0x10) and French (0x0C).
        /// </remarks>
        /// <param name="Error">A TPCANStatus error code.</param>
        /// <param name="Language">Indicates a 'Primary language ID.</param>
        /// <param name="StringBuffer">Buffer for the text (must be at least 256 in length).</param>
        /// <returns>A TPCANStatus error code.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here.")]
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_GetErrorText")]
        public static extern TPCANStatus GetErrorText(
            [MarshalAs(UnmanagedType.U4)] TPCANStatus Error,
            ushort Language,
            StringBuilder StringBuffer);

        /// <summary>
        ///     Gets the current status of a PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport(@"PCANBasic.dll", EntryPoint = "CAN_GetStatus")]
        public static extern TPCANStatus GetStatus([MarshalAs(UnmanagedType.U2)] ushort Channel);

        /// <summary>
        ///     Retrieves a PCAN Channel value.
        /// </summary>
        /// <remarks>
        ///     Parameters can be present or not according with the kind
        ///     of Hardware (PCAN Channel) being used. If a parameter is not available,
        ///     a PCAN_ERROR_ILLPARAMTYPE error will be returned.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Parameter">The TPCANParameter parameter to get.</param>
        /// <param name="StringBuffer">Buffer for the parameter value.</param>
        /// <param name="BufferLength">Size in bytes of the buffer.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_GetValue")]
        public static extern TPCANStatus GetValue(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            [MarshalAs(UnmanagedType.U1)] TpcanParameter Parameter,
            StringBuilder StringBuffer,
            uint BufferLength);

        /// <summary>
        ///     Retrieves a PCAN Channel value.
        /// </summary>
        /// <remarks>
        ///     Parameters can be present or not according with the kind
        ///     of Hardware (PCAN Channel) being used. If a parameter is not available,
        ///     a PCAN_ERROR_ILLPARAMTYPE error will be returned.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Parameter">The TPCANParameter parameter to get.</param>
        /// <param name="NumericBuffer">Buffer for the parameter value.</param>
        /// <param name="BufferLength">Size in bytes of the buffer.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_GetValue")]
        public static extern TPCANStatus GetValue(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            [MarshalAs(UnmanagedType.U1)] TpcanParameter Parameter,
            out uint NumericBuffer,
            uint BufferLength);

        /// <summary>
        ///     Retrieves a PCAN Channel value.
        /// </summary>
        /// <remarks>
        ///     Parameters can be present or not according with the kind
        ///     of Hardware (PCAN Channel) being used. If a parameter is not available,
        ///     a PCAN_ERROR_ILLPARAMTYPE error will be returned.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Parameter">The TPCANParameter parameter to get.</param>
        /// <param name="NumericBuffer">Buffer for the parameter value.</param>
        /// <param name="BufferLength">Size in bytes of the buffer.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_GetValue")]
        public static extern TPCANStatus GetValue(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            [MarshalAs(UnmanagedType.U1)] TpcanParameter Parameter,
            out ulong NumericBuffer,
            uint BufferLength);

        /// <summary>
        ///     Initializes a PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Btr0Btr1">The speed for the communication (BTR0BTR1 code).</param>
        /// <param name="HwType">NON PLUG and PLAY: The type of hardware and operation mode.</param>
        /// <param name="IOPort">NON PLUG and PLAY: The I/O address for the parallel port.</param>
        /// <param name="Interrupt">NON PLUG and PLAY: Interrupt number of the parallel port.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Initialize")]
        public static extern TPCANStatus Initialize(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            [MarshalAs(UnmanagedType.U2)] TPCANBaudrate Btr0Btr1,
            [MarshalAs(UnmanagedType.U1)] TPCANType HwType,
            uint IOPort,
            ushort Interrupt);

        /// <summary>
        ///     Initializes a PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Btr0Btr1">The speed for the communication (BTR0BTR1 code).</param>
        /// <returns>A TPCANStatus error code.</returns>
        public static TPCANStatus Initialize(ushort Channel, TPCANBaudrate Btr0Btr1)
        {
            return Initialize(Channel, Btr0Btr1, 0, 0, 0);
        }

        /// <summary>
        ///     Initializes a FD capable PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a FD capable PCAN Channel.</param>
        /// <param name="BitrateFD">The speed for the communication (FD bit rate string).</param>
        /// <remarks>
        ///     See PCAN_BR_* values
        ///     Bit rate string must follow the following construction rules:
        ///     * parameters and values must be separated by '='
        ///     * Couples of Parameter/value must be separated by ','
        ///     * Following Parameter must be filled out: f_clock, data_brp, data_sjw, data_tseg1, data_tseg2,
        ///     nom_brp, nom_sjw, nom_tseg1, nom_tseg2.
        ///     * Following Parameters are optional (not used yet): data_ssp_offset, nom_sam.
        /// </remarks>
        /// <example>F_clock=80000000,nom_brp=10,nom_tseg1=5,nom_tseg2=2,nom_sjw=1,data_brp=4,data_tseg1=7,data_tseg2=2,data_sjw=1.</example>
        /// <returns>A TPCANStatus error code.</returns>
        [SuppressMessage(
            "StyleCop.CSharp.DocumentationRules",
            "SA1630:DocumentationTextMustContainWhitespace",
            Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage(
            "StyleCop.CSharp.DocumentationRules",
            "SA1650:ElementDocumentationMustBeSpelledCorrectly",
            Justification = "Reviewed. Suppression is OK here.")]
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_InitializeFD")]
        public static extern TPCANStatus InitializeFD([MarshalAs(UnmanagedType.U2)] ushort Channel, string BitrateFD);

        /// <summary>
        ///     Reads a CAN message from the receive queue of a PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="MessageBuffer">A TPCANMSG structure buffer to store the CAN message.</param>
        /// <param name="TimestampBuffer">
        ///     A TPCANTimestamp structure buffer to get.
        ///     the reception time of the message.
        /// </param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Read")]
        public static extern TPCANStatus Read(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            out TpcanMsg MessageBuffer,
            out TpcanTimestamp TimestampBuffer);

        /// <summary>
        ///     Reads a CAN message from the receive queue of a PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="MessageBuffer">A TPCANMSG structure buffer to store the CAN message.</param>
        /// <returns>A TPCANStatus error code.</returns>
        public static TPCANStatus Read(ushort Channel, out TpcanMsg MessageBuffer)
        {
            return Read(Channel, out MessageBuffer, IntPtr.Zero);
        }

        /// <summary>
        ///     Reads a CAN message from the receive queue of a FD capable PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a FD capable PCAN Channel.</param>
        /// <param name="MessageBuffer">A TPCANMSGFD structure buffer to store the CAN message.</param>
        /// <param name="TimestampBuffer">
        ///     A TPCANTimestampFD buffer to get the
        ///     reception time of the message.
        /// </param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_ReadFD")]
        public static extern TPCANStatus ReadFD(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            out TpcanMsgFd MessageBuffer,
            out ulong TimestampBuffer);

        /// <summary>
        ///     Reads a CAN message from the receive queue of a FD capable PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a FD capable PCAN Channel.</param>
        /// <param name="MessageBuffer">A TPCANMSGFD structure buffer to store the CAN message.</param>
        /// <returns>A TPCANStatus error code.</returns>
        public static TPCANStatus ReadFD(ushort Channel, out TpcanMsgFd MessageBuffer)
        {
            return ReadFD(Channel, out MessageBuffer, IntPtr.Zero);
        }

        /// <summary>
        ///     Resets the receive and transmit queues of the PCAN Channel.
        /// </summary>
        /// <remarks>A reset of the CAN controller is not performed.</remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Reset")]
        public static extern TPCANStatus Reset([MarshalAs(UnmanagedType.U2)] ushort Channel);

        /// <summary>
        ///     Configures or sets a PCAN Channel value.
        /// </summary>
        /// <remarks>
        ///     Parameters can be present or not according with the kind
        ///     of Hardware (PCAN Channel) being used. If a parameter is not available,
        ///     a PCAN_ERROR_ILLPARAMTYPE error will be returned.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Parameter">The TPCANParameter parameter to set.</param>
        /// <param name="NumericBuffer">Buffer with the value to be set.</param>
        /// <param name="BufferLength">Size in bytes of the buffer.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_SetValue")]
        public static extern TPCANStatus SetValue(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            [MarshalAs(UnmanagedType.U1)] TpcanParameter Parameter,
            ref uint NumericBuffer,
            uint BufferLength);

        /// <summary>
        ///     Configures or sets a PCAN Channel value.
        /// </summary>
        /// <remarks>
        ///     Parameters can be present or not according with the kind
        ///     of Hardware (PCAN Channel) being used. If a parameter is not available,
        ///     a PCAN_ERROR_ILLPARAMTYPE error will be returned.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Parameter">The TPCANParameter parameter to set.</param>
        /// <param name="NumericBuffer">Buffer with the value to be set.</param>
        /// <param name="BufferLength">Size in bytes of the buffer.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_SetValue")]
        public static extern TPCANStatus SetValue(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            [MarshalAs(UnmanagedType.U1)] TpcanParameter Parameter,
            ref ulong NumericBuffer,
            uint BufferLength);

        /// <summary>
        ///     Configures or sets a PCAN Channel value.
        /// </summary>
        /// <remarks>
        ///     Parameters can be present or not according with the kind
        ///     of Hardware (PCAN Channel) being used. If a parameter is not available,
        ///     a PCAN_ERROR_ILLPARAMTYPE error will be returned.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="Parameter">The parameter.</param>
        /// <param name="StringBuffer">Buffer with the value to be set.</param>
        /// <param name="BufferLength">Size in bytes of the buffer.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_SetValue")]
        public static extern TPCANStatus SetValue(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            [MarshalAs(UnmanagedType.U1)] TpcanParameter Parameter,
            [MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 3)] string StringBuffer,
            uint BufferLength);

        /// <summary>
        ///     Uninitializes one or all PCAN Channels initialized by CAN_Initialize.
        /// </summary>
        /// <remarks>
        ///     Giving the TPCANHandle value "PCAN_NONEBUS",
        ///     uninitialize all initialized channels.
        /// </remarks>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Uninitialize")]
        public static extern TPCANStatus Uninitialize([MarshalAs(UnmanagedType.U2)] ushort Channel);

        /// <summary>
        ///     Transmits a CAN message.
        /// </summary>
        /// <param name="Channel">The handle of a PCAN Channel.</param>
        /// <param name="MessageBuffer">A TPCANMSG buffer with the message to be sent.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Write")]
        public static extern TPCANStatus Write(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            ref TpcanMsg MessageBuffer);

        /// <summary>
        ///     Transmits a CAN message over a FD capable PCAN Channel.
        /// </summary>
        /// <param name="Channel">The handle of a FD capable PCAN Channel.</param>
        /// <param name="MessageBuffer">A TPCANMSGFD buffer with the message to be sent.</param>
        /// <returns>A TPCANStatus error code.</returns>
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_WriteFD")]
        public static extern TPCANStatus WriteFD(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            ref TpcanMsgFd MessageBuffer);

        #endregion

        #region Private Methods

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Read")]
        private static extern TPCANStatus Read(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            out TpcanMsg MessageBuffer,
            IntPtr bufferPointer);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_ReadFD")]
        private static extern TPCANStatus ReadFD(
            [MarshalAs(UnmanagedType.U2)] ushort Channel,
            out TpcanMsgFd MessageBuffer,
            IntPtr TimestampBuffer);

        #endregion
    }
}