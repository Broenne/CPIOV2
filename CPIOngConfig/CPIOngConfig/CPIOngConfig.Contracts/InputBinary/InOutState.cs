namespace CPIOngConfig.Contracts.InputBinary
{
    using System.Windows.Media;

    using Prism.Mvvm;

    /// <summary>
    ///     The in out state.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class InOutState : BindableBase
    {
        private SolidColorBrush color;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="InOutState" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public InOutState(int position, SolidColorBrush color)
        {
            this.Position = position;
            this.Color = color;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public SolidColorBrush Color
        {
            get => this.color;

            set => this.SetProperty(ref this.color, value);
        }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public int Position { get; set; }

        #endregion
    }
}