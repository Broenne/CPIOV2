namespace Hal.PeakCan.Contracts.Basics
{
    /// <summary>
    /// The service for read can message.
    /// </summary>
    public interface IReadCanMessage
    {
        /// <summary>
        /// Does the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        byte[] Do(uint id);
    }
}