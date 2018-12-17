namespace CPIOngConfig.Contracts.InputBinary
{
    using System;

    /// <summary>
    /// The input binary event handler interface.
    /// </summary>
    public interface IInputBinaryEventHandler
    {
        #region Events

        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<InputBinaryEventArgs> EventIsReached;

        #endregion

        #region Public Methods

        /// <summary>
        /// Raises the <see cref="E:Reached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="InputBinaryEventArgs"/> instance containing the event data.</param>
        void OnReached(InputBinaryEventArgs e);

        #endregion
    }
}