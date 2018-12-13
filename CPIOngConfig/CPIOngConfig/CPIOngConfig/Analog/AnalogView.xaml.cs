namespace CPIOngConfig.Analog
{
    using System.Windows.Controls;

    /// <summary>
    ///     Interaction logic for AnalogView.xaml
    /// </summary>
    public partial class AnalogView : UserControl, IAnalogView
    {
        #region Constructor

        public AnalogView(IAnalogViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}