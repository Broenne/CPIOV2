namespace CPIOngConfig.Contracts.ConfigInputs
{
    /// <summary>
    /// The interface for configure input view model.
    /// </summary>
    public interface IConfigureInputsViewModel
    {
        /// <summary>
        /// Sets the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        void SetChannel(uint channel);
    }
}