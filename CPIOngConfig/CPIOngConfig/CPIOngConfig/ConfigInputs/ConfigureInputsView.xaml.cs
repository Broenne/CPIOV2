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
        ///     Initializes a new instance of the <see cref="ConfigureInputsView" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public ConfigureInputsView(IConfigureInputsViewModel vm)
        {
            this.InitializeComponent();
            this.ConfigureInputsViewModel = vm;
            this.DataContext = this.ConfigureInputsViewModel;
        }

        #endregion

        #region Properties

        private IConfigureInputsViewModel ConfigureInputsViewModel { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the data context.
        /// </summary>
        /// <returns>Return the view model.</returns>
        public IConfigureInputsViewModel GetDataContext()
        {
            return this.ConfigureInputsViewModel;
        }

        #endregion
    }
}