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

        void OnReached(FlipFlopEventArgs e);
    }
}