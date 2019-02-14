namespace CPIOngConfig.Contracts.Pulse
{
    using Prism.Mvvm;

    /// <summary>
    ///     The time pulse object.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class TimePulse : BindableBase
    {
        private double tim;

        private double volume;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePulse" /> class.
        /// </summary>
        /// <param name="tim">The time info.</param>
        /// <param name="volume">The volume.</param>
        public TimePulse(double tim, double volume)
        {
            this.Tim = tim;
            this.Volume = volume;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tim.
        /// </summary>
        /// <value>
        /// The time info.
        /// </value>
        public double Tim
        {
            get => this.tim;
            set => this.SetProperty(ref this.tim, value);
        }

        /// <summary>
        /// Gets or sets the tim.
        /// </summary>
        /// <value>
        /// The time info.
        /// </value>
        public double Volume
        {
            get => this.volume;
            set => this.SetProperty(ref this.volume, value);
        }

        #endregion
    }
}