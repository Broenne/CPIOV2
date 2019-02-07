namespace CPIOngConfig.Error
{
    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.Error;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The error hardware view model.
    /// </summary>
    /// <seealso cref="CPIOngConfig.Contracts.Error.IErrorHardwareViewModel" />
    public class ErrorHardwareViewModel : BindableBase, IErrorHardwareViewModel
    {
        private string rawErrorFildData;

        #region Constructor

        public ErrorHardwareViewModel(ILogger logger, IAliveEventHandler aliveEventHandler)
        {
            this.Logger = logger;
            this.AliveEventHandler = aliveEventHandler;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the raw error fild data.
        /// </summary>
        /// <value>
        /// The raw error fild data.
        /// </value>
        public string RawErrorFildData
        {
            get => this.rawErrorFildData;
            set => this.SetProperty(ref this.rawErrorFildData, value);
        }

        private IAliveEventHandler AliveEventHandler { get; }

        private ILogger Logger { get; }

        #endregion
    }
}