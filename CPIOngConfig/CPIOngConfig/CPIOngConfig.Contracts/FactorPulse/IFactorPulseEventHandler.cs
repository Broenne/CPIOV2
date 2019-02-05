namespace CPIOngConfig.Contracts.FactorPulse
{
    using System;

    /// <summary>
    /// The factor pulse event handler.
    /// </summary>
    public interface IFactorPulseEventHandler
    {
        #region Events

        /// <summary>
        ///     Occurs when [event is reached].
        /// </summary>
        event EventHandler<FactorPulseEventArgs> EventIsReached;

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [reached].
        /// </summary>
        /// <param name="args">The <see cref="FactorPulseEventArgs"/> instance containing the event data.</param>
        void OnReached(FactorPulseEventArgs args);

        #endregion
    }
}