namespace ConfigLogicLayer.Contracts.Analog
{
    using System;

    /// <summary>
    /// The analog event arguments.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class AnalogEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnalogEventArgs" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="digits">The digits.</param>
        /// <param name="millivoltage">The milli voltage.</param>
        public AnalogEventArgs(uint channel, uint digits, uint millivoltage)
        {
            this.Channel = channel;
            this.Digits = digits;
            this.Millivoltage = millivoltage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public uint Channel { get; }

        /// <summary>
        /// Gets the digits.
        /// </summary>
        /// <value>
        /// The digits.
        /// </value>
        public uint Digits { get; }

        /// <summary>
        /// Gets the millivoltage.
        /// </summary>
        /// <value>
        /// The millivoltage.
        /// </value>
        public uint Millivoltage { get; }

        #endregion
    }
}