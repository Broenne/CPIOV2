namespace CPIOngConfig.Alive
{
    using CPIOngConfig.Contracts.Alive;

    /// <summary>
    ///     Interaction logic for AliveView.
    /// </summary>
    public partial class AliveView : IAliveView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AliveView" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public AliveView(IAliveViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}