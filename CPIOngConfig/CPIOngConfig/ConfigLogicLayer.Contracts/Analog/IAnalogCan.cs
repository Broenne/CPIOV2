namespace ConfigLogicLayer.Contracts.Analog
{
    /// <summary>
    /// The analog can bus interface.
    /// </summary>
    public interface IAnalogCan
    {
        /// <summary>
        /// Triggers the specified channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        void Trigger(uint channel);
    }
}