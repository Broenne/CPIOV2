namespace Hal.PeakCan.Contracts.Basics
{
    /// <summary>
    ///     The read CAN message interface.
    /// </summary>
    public interface IReadCanMessage
    {
        #region Public Methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns>The message event handler.</returns>
        IReadCanMessageEvent Start();

        #endregion
    }
}