namespace ConfigLogicLayer.Contracts.DigitalInputState
{
    /// <summary>
    ///     The active debug mode.
    /// </summary>
    public interface IActivateDebugMode
    {
        #region Public Methods

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        void Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        void Deactivate();

        #endregion
    }
}