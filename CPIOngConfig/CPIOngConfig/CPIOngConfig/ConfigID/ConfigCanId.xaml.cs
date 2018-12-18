namespace CPIOngConfig.ConfigID
{
    using CPIOngConfig.Contracts.ConfigId;

    /// <summary>
    ///     Interaction logic for ConfigCanId.
    /// </summary>
    public partial class ConfigCanId : IConfigCanId
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigCanId" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public ConfigCanId(IConfigCanIdViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}