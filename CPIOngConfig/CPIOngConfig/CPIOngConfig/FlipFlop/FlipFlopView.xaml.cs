namespace CPIOngConfig.FlipFlop
{
    using CPIOngConfig.Contracts.FlipFlop;

    /// <summary>
    ///     Interaction logic for FlipFlopView.
    /// </summary>
    public partial class FlipFlopView : IFlipFlopView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FlipFlopView" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public FlipFlopView(IFlipFlopViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        #endregion
    }
}