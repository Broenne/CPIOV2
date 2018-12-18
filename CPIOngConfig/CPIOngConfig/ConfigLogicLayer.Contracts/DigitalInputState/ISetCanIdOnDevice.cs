namespace ConfigLogicLayer.Contracts.DigitalInputState
{
    /// <summary>
    /// The interface for set can ID on device.
    /// </summary>
    public interface ISetCanIdOnDevice
    {
        /// <summary>
        /// Does the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Do(byte id);
    }
}