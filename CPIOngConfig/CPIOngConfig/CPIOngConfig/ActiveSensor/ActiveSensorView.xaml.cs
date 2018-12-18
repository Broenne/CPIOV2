namespace CPIOngConfig.ActiveSensor
{
    using System.Windows.Controls;

    using CPIOngConfig.Contracts.ActiveSensor;

    /// <summary>
    ///     Interaction logic for ActiveSensorView.
    /// </summary>
    public partial class ActiveSensorView : IActiveSensorView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveSensorView" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public ActiveSensorView(IActiveSensorViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}