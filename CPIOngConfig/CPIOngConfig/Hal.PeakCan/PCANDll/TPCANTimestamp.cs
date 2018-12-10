namespace HardwareAbstaction.PCAN.PCANDll
{
    /// <summary>
    /// Represents a timestamp of a received PCAN message.
    /// Total Microseconds = micros + 1000 * millis + 0x100000000 * 1000 * millis_overflow.
    /// </summary>
    public struct TpcanTimestamp
    {
        /// <summary>
        /// Base-value: milliseconds: 0.. 2^32-1.
        /// </summary>
        public uint Millis;

        /// <summary>
        /// Roll a rounds of millis.
        /// </summary>
        public ushort MillisOverflow;

        /// <summary>
        /// Microseconds: 0..999.
        /// </summary>
        public ushort Micros;
    }
}