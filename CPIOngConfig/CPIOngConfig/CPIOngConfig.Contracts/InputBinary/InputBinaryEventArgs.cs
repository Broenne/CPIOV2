namespace CPIOngConfig.Contracts.InputBinary
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The input binary.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class InputBinaryEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InputBinaryEventArgs"/> class.
        /// </summary>
        public InputBinaryEventArgs()
        {
            this.Store = new List<DataBinary>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public List<DataBinary> Store { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="val">If set to <c>true</c> [value].</param>
        public void Add(uint channel, bool val)
        {
            this.Store.Add(new DataBinary(channel, val));
        }

        #endregion
    }
}