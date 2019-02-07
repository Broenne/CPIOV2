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
        /// Initializes a new instance of the <see cref="AliveEventArgs" /> class.
        /// </summary>
        /// <param name="versionCpioNg">The version CPIO ng.</param>
        public AliveEventArgs(Version versionCpioNg)
        {
            this.VersionCpioNg = versionCpioNg;
            this.DateTime = DateTime.Now;
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; }
        
        /// <summary>
        /// Gets the version CPIO ng.
        /// </summary>
        /// <value>
        /// The version CPIO ng.
        /// </value>
        public Version VersionCpioNg { get; } 
    }
}