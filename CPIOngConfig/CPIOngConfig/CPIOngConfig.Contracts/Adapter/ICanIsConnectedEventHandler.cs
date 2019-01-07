namespace CPIOngConfig.Contracts.Adapter
{
    using System;

    /// <summary>
    ///     The event for can is connected.
    /// </summary>
    public interface ICanIsConnectedEventHandler
    {
        #region Events

        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<bool> EventIsReached;

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [reached].
        /// </summary>
        /// <param name="e">If set to <c>true</c> [e].</param>
        void OnReached(bool e);

        #endregion
    }
}