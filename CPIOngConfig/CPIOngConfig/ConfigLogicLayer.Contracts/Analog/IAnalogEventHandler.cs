namespace ConfigLogicLayer.Contracts.Analog
{
    using System;

    /// <summary>
    /// The analog event handler.
    /// </summary>
    public interface IAnalogEventHandler
    {
        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<AnalogEventArgs> EventIsReached;

        /// <summary>
        /// Raises the <see cref="E:Reached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="AnalogEventArgs"/> instance containing the event data.</param>
        void OnReached(AnalogEventArgs e);
    }
}