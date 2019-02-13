namespace CPIOngConfig.Contracts.Pulse
{
    using System;

    /// <summary>
    /// The pulse event arguments.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class PulseEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseEventArgs" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="stamp">The stamp.</param>
        /// <param name="checkSum">The check sum.</param>
        public PulseEventArgs(uint channel, uint stamp, byte checkSum)
        {
            this.Channel = (int)channel;
            this.Stamp = stamp;
            this.CheckSum = checkSum;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public int Channel { get; }

        /// <summary>
        /// Gets the stamp.
        /// </summary>
        /// <value>
        /// The stamp.
        /// </value>
        public uint Stamp { get; }

        /// <summary>
        /// Gets the check sum.
        /// </summary>
        /// <value>
        /// The check sum.
        /// </value>
        public byte CheckSum { get; }

        #endregion
    }
}