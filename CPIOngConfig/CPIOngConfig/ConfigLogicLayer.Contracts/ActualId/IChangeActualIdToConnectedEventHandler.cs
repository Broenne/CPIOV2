namespace ConfigLogicLayer.Contracts.ActualId
{
    using System;

    /// <summary>
    /// The event handler for change connected node id.
    /// </summary>
    public interface IChangeActualIdToConnectedEventHandler
    {
        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<ushort> EventIsReached;

        /// <summary>
        /// Called when [reached].
        /// </summary>
        /// <param name="e">The event argument.</param>
        void OnReached(ushort e);
    }
}