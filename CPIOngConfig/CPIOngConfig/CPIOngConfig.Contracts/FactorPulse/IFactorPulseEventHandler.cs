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
        /// <param name="volumePerTimeSlot">The volume per time slot.</param>
        void OnReached(FactorPulseEventArgs args);

        #endregion
    }
}