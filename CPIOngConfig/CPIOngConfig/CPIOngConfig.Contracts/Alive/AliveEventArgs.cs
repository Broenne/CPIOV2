namespace CPIOngConfig.Contracts.Alive
{
    using System;

    /// <summary>
    ///     The alive event arguments.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class AliveEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AliveEventArgs"/> class.
        /// </summary>
        public AliveEventArgs()
        {
            this.DateTime = DateTime.Now;
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; }
    }
}