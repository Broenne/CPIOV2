namespace ConfigLogicLayer.Contracts.DigitalInputState
{
    /// <summary>
    ///     The handle inputs event.
    /// </summary>
    public interface IHandleInputs
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}