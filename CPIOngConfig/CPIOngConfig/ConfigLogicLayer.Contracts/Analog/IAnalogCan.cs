namespace ConfigLogicLayer.Contracts.Analog
{
    using System.Threading.Tasks;

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

        /// <summary>
        /// Triggers the run all.
        /// </summary>
        /// <returns>Return the task scheduler.</returns>
        Task TriggerRunAll();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}