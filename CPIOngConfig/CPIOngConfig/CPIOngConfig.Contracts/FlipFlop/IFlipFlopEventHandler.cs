namespace CPIOngConfig.Contracts.FlipFlop
{
    using System;


    /// <summary>
    /// 
    /// </summary>
    public interface IFlipFlopEventHandler
    {
        /// <summary>
        /// Occurs when [event is reached].
        /// </summary>
        event EventHandler<FlipFlopEventArgs> EventIsReached;

        /// <summary>
        /// Raises the <see cref="E:Reached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="FlipFlopEventArgs"/> instance containing the event data.</param>
        void OnReached(FlipFlopEventArgs e);
    }
}