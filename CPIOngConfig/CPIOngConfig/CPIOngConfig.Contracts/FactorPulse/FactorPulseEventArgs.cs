namespace CPIOngConfig.Contracts.FactorPulse
{
    using System;

    /// <summary>
    ///     The factor pulse event arguments.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class FactorPulseEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="FactorPulseEventArgs" /> class.
        /// </summary>
        /// <param name="timeBase">The time base.</param>
        /// <param name="volumePerTimeSlot">The volume per time slot.</param>
        public FactorPulseEventArgs(DestinationTimeBase timeBase, double volumePerTimeSlot)
        {
            this.TimeBase = timeBase;
            this.VolumePerTimeSlot = volumePerTimeSlot;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the time base.
        /// </summary>
        /// <value>
        ///     The time base.
        /// </value>
        public DestinationTimeBase TimeBase { get; }

        /// <summary>
        ///     Gets the volume per time slot.
        /// </summary>
        /// <value>
        ///     The volume per time slot.
        /// </value>
        public double VolumePerTimeSlot { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the factor.
        /// </summary>
        /// <returns>Return the scale factor.</returns>
        public double GetFactor()
        {
            // aktuell t in 0,1 ms

            // var timDifInMs = timDifin0D1Ms / 10.0;
            // var timeInSecond = timDifInMs / 1000.0;
            // var tinmeInMinutes = timeInSecond / 60;

            // info mb: bezogen auf 0,1ms
            switch (this.TimeBase)
            {
                case DestinationTimeBase.Raw:
                    return 1;
                case DestinationTimeBase.Hour:
                    return 1.0 / (10 * 1000 * 60 * 60);
                case DestinationTimeBase.Minute:
                    return 1.0 / (10 * 1000 * 60);
                case DestinationTimeBase.Second:
                    return 1.0 / (10 * 1000);
                case DestinationTimeBase.Millisecond:
                    return 1.0 / 10;
                default:
                    return 1.0;
            }
        }

        #endregion
    }
}