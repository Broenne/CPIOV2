namespace CPIOngConfig.Alive
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    using CPIOngConfig.Contracts.Alive;

    using Prism.Mvvm;

    /// <summary>
    ///     The alive view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.Alive.IAliveViewModel" />
    public class AliveViewModel : BindableBase, IAliveViewModel
    {
        private readonly SolidColorBrush gray = new SolidColorBrush(Colors.Gray);

        private readonly SolidColorBrush green = new SolidColorBrush(Colors.Green);

        private string bugfix;

        private SolidColorBrush color;

        private string lastUpdateTime;

        private string major;

        private string minor;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AliveViewModel" /> class.
        /// </summary>
        /// <param name="aliveEventHandler">The alive event handler.</param>
        public AliveViewModel(IAliveEventHandler aliveEventHandler)
        {
            aliveEventHandler.EventIsReached += this.AliveEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the bug fix.
        /// </summary>
        /// <value>
        /// The bug fix.
        /// </value>
        public string Bugfix
        {
            get => this.bugfix;

            set => this.SetProperty(ref this.bugfix, value);
        }

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

        /// <summary>
        /// Gets or sets the major.
        /// </summary>
        /// <value>
        /// The major.
        /// </value>
        public string Major
        {
            get => this.major;

            set => this.SetProperty(ref this.major, value);
        }

        /// <summary>
        /// Gets or sets the minor.
        /// </summary>
        /// <value>
        /// The minor.
        /// </value>
        public string Minor
        {
            get => this.minor;

            set => this.SetProperty(ref this.minor, value);
        }

        #endregion

        #region Private Methods

        private void AliveEventHandler_EventIsReached(object sender, AliveEventArgs e)
        {
            try
            {
                this.Color = this.Color == this.green ? this.gray : this.green;
                this.LastUpdateTime = e.DateTime.ToString(CultureInfo.InvariantCulture);

                this.Major = e.VersionCpioNg.Major.ToString();
                this.Minor = e.VersionCpioNg.Minor.ToString();
                this.Bugfix = e.VersionCpioNg.Build.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}