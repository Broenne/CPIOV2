namespace CPIOngConfig.Contracts.FlipFlop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The flip flop event arguments.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class FlipFlopEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="FlipFlopEventArgs" /> class.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        public FlipFlopEventArgs(IEnumerable<byte> rawData)
        {
            this.RawData = rawData.ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the raw data.
        /// </summary>
        /// <value>
        ///     The raw data.
        /// </value>
        public IReadOnlyList<byte> RawData { get; }

        #endregion
    }
}