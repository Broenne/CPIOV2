namespace CPIOngConfig.Contracts.Alive
{
    using System;
    
    /// <summary>
    /// The alive event handler.
    /// </summary>
    public interface IAliveEventHandler
    {
        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<AliveEventArgs> EventIsReached;

        /// <summary>
        /// Raises the <see cref="E:Reached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="AliveEventArgs"/> instance containing the event data.</param>
        void OnReached(AliveEventArgs e);
    }
}