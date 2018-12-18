namespace CPIOngConfig.Alive
{
    using System;
    using System.Windows.Media;

    using CPIOngConfig.Contracts.Alive;

    using Prism.Mvvm;

    /// <summary>
    /// The alive view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.Alive.IAliveViewModel" />
    public class AliveViewModel : BindableBase, IAliveViewModel
    {
        private SolidColorBrush color;

        private string lastUpdateTime;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AliveViewModel" /> class.
        /// </summary>
        public AliveViewModel()
        {
            this.LastUpdateTime = DateTime.Now.ToString();
            this.Color = new SolidColorBrush(Colors.Green);
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
        ///     Gets or sets the last update time.
        /// </summary>
        /// <value>
        ///     The last update time.
        /// </value>
        public string LastUpdateTime
        {
            get => this.lastUpdateTime;

            set => this.SetProperty(ref this.lastUpdateTime, value);
        }

        #endregion
    }
}