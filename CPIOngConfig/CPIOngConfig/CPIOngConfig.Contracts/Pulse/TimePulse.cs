namespace CPIOngConfig.Contracts.Pulse
{
    using Prism.Mvvm;

    /// <summary>
    ///     The time pulse object.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class TimePulse : BindableBase
    {
        private string tim;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePulse"/> class.
        /// </summary>
        /// <param name="tim">The time info.</param>
        public TimePulse(string tim)
        {
            this.Tim = tim;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tim.
        /// </summary>
        /// <value>
        /// The time info.
        /// </value>
        public string Tim
        {
            get => this.tim;
            set => this.SetProperty(ref this.tim, value);
        }

        #endregion
    }
}