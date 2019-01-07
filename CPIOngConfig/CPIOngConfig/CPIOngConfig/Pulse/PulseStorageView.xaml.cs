namespace CPIOngConfig.Pulse
{
    using CPIOngConfig.Contracts.Pulse;

    /// <summary>
    ///     Interaction logic for PulseStorageView.
    /// </summary>
    public partial class PulseStorageView : IPulseStorageView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseStorageView" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public PulseStorageView(IPulseStorageViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}