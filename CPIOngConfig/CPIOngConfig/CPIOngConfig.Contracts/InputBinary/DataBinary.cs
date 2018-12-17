namespace CPIOngConfig.Contracts.InputBinary
{
    /// <summary>
    /// The data binary.
    /// </summary>
    public class DataBinary
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBinary"/> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="value">If set to <c>true</c> [value].</param>
        public DataBinary(uint channel, bool value)
        {
            this.Channel = channel;
            this.Value = value;
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
        /// Gets a value indicating whether this <see cref="DataBinary"/> is value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool Value { get; }

        #endregion
    }
}