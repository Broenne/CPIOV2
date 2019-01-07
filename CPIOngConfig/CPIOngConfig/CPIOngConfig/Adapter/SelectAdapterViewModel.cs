namespace CPIOngConfig.Adapter
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Autofac;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.Adapter;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The select adapter view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.Adapter.ISelectAdapterViewModel" />
    public class SelectAdapterViewModel : BindableBase, ISelectAdapterViewModel
    {
        private CanAdapter canAdapter;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAdapterViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="scope">The scope.</param>
        public SelectAdapterViewModel(ILogger logger, ILifetimeScope scope)
        {
            this.Logger = logger;
            this.Scope = scope;
            this.ConnectCommand = new RelayCommand(this.ConnectCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the can adapter.
        /// </summary>
        /// <value>
        /// The can adapter.
        /// </value>
        public CanAdapter CanAdapter
        {
            get => this.canAdapter;
            set => this.SetProperty(ref this.canAdapter, value);
        }

        /// <summary>
        ///     Gets the connect command.
        /// </summary>
        /// <value>
        ///     The connect command.
        /// </value>
        public ICommand ConnectCommand { get; }

        private ILogger Logger { get; }

        private ILifetimeScope Scope { get; }

        #endregion

        #region Private Methods

        private void ConnectCommandAction(object obj)
        {
            try
            {
                switch (this.CanAdapter)
                {
                    case CanAdapter.PeakUsb:
                        var handleInputs = this.Scope.Resolve<IHandleInputs>();
                        handleInputs.Start();
                        break;
                    case CanAdapter.Esd:
                        MessageBox.Show("Noch nicht implemtiert");
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}