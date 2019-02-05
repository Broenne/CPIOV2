namespace CPIOngConfig.FactorPulse
{
    using CPIOngConfig.Contracts.FactorPulse;

    /// <summary>
    ///     Interaction logic for FactorPulseView.
    /// </summary>
    public partial class FactorPulseView : IFactorPulseView
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="FactorPulseView" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public FactorPulseView(IFactorPulseViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}