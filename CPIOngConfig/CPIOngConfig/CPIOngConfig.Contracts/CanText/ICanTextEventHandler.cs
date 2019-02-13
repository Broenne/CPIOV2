namespace CPIOngConfig.Contracts.CanText
{
    using System;

    /// <summary>
    ///     The can text event handler.
    /// </summary>
    public interface ICanTextEventHandler
    {
        #region Events

        /// <summary>
        ///     Occurs when [event is reached].
        /// </summary>
        event EventHandler<byte> EventIsReached;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Called when [reached].
        /// </summary>
        /// <param name="e">The event argument.</param>
        void OnReached(byte e);

        #endregion
    }
}