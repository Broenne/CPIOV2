namespace CPIOngConfig.Error
{
    using CPIOngConfig.Contracts.Error;

    /// <summary>
    ///     Interaction logic for ErrorHardwareView.
    /// </summary>
    public partial class ErrorHardwareView : IErrorHardwareView
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorHardwareView" /> class.
        /// </summary>
        /// <param name="vm">
        ///     The view model     .
        /// </param>
        public ErrorHardwareView(IErrorHardwareViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}