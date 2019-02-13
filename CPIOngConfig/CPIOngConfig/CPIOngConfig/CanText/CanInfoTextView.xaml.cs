namespace CPIOngConfig.CanText
{
    using CPIOngConfig.Contracts.CanText;

    /// <summary>
    ///     Interaction logic for CanInfoTextView.
    /// </summary>
    public partial class CanInfoTextView : ICanInfoTextView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CanInfoTextView"/> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public CanInfoTextView(ICanInfoTextViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}