namespace CPIOngConfig.InputBinary
{
    using CPIOngConfig.Contracts.InputBinary;

    /// <summary>
    ///     Interaction logic for InputBinaryView.
    /// </summary>
    public partial class InputBinaryView : IInputBinaryView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InputBinaryView"/> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public InputBinaryView(IInputBinaryViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}