namespace ConfigLogicLayer.Contracts.DigitalInputState
{
    /// <summary>
    /// The reset flip flop.
    /// </summary>
    public interface IResetFlipFlop
    {
        /// <summary>
        /// Resets all.
        /// </summary>
        void ResetAll();

        /// <summary>
        /// Resets the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        void ResetChannel(uint channel);
    }
}