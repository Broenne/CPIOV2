﻿namespace ConfigLogicLayer.Contracts.DigitalInputState
{
    using CPIOngConfig.Contracts.ConfigInputs;

    /// <summary>
    /// The interface for set active sensor to detect.
    /// </summary>
    public interface ISetActiveSensorToDetect
    {
        /// <summary>
        /// Does the specified modi.
        /// </summary>
        /// <param name="modi">The modi info.</param>
        void Do(Modi modi);
    }
}