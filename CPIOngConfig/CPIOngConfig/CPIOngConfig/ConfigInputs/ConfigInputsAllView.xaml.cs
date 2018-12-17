namespace CPIOngConfig.ConfigInputs
{

    using CPIOngConfig.Contracts.ConfigInputs;

    /// <summary>
    ///     Interaction logic for ConfigInputsAllView.
    /// </summary>
    public partial class ConfigInputsAllView : IConfigInputsAllView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigInputsAllView"/> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public ConfigInputsAllView(IConfigInputsAllViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}