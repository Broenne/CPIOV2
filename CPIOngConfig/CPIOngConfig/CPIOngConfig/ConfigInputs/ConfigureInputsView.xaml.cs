namespace CPIOngConfig.ConfigInputs
{
    using CPIOngConfig.Contracts.ConfigInputs;

    /// <summary>
    ///     Interaction logic for ConfigureInputsView.
    /// </summary>
    public partial class ConfigureInputsView : IConfigureInputsView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureInputsView"/> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public ConfigureInputsView(IConfigureInputsViewModel vm)
        {
            this.InitializeComponent();
            this.ConfigureInputsViewModel = vm;
            this.DataContext = this.ConfigureInputsViewModel;
        }

        private IConfigureInputsViewModel ConfigureInputsViewModel { get; }

        #endregion

        /// <summary>
        /// Gets the data context.
        /// </summary>
        /// <returns>Return the view mdoel.</returns>
        public IConfigureInputsViewModel GetDataContext()
        {
            return this.ConfigureInputsViewModel;
        }
    }
}