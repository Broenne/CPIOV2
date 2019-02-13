namespace ConfigLogicLayer.Contracts.Text
{
    /// <summary>
    /// The interface for text.
    /// </summary>
    public interface IText
    {
        /// <summary>
        /// Requests the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="positionInRow">The position in row.</param>
        void Request(byte position, byte positionInRow = 0);
    }
}