namespace ConfigLogicLayer.Contracts.ActualId
{
    /// <summary>
    /// The interface for get actual node id.
    /// </summary>
    public interface IGetActualNodeId
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Return the ID.</returns>
        ushort Get();
    }
}