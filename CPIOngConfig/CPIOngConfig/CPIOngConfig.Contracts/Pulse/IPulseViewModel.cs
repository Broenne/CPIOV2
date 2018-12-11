namespace CPIOngConfig.Contracts.Pulse
{
    using System.Collections.Generic;

    /// <summary>
    /// The interface puls view model.
    /// </summary>
    public interface IPulseViewModel
    {
        /// <summary>
        /// Gets the pulse data for view list.
        /// </summary>
        /// <value>
        /// The pulse data for view list.
        /// </value>
        List<PulseDataForView> PulseDataForViewList { get; }
    }
}