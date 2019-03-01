namespace CPIOngConfig.Adapter
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Autofac;

    using ConfigLogicLayer.Contracts.ActualId;
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

        private bool nodeIdChangeIsEnabled;

        private ushort nodeIdValue;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAdapterViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="changeActualIdToConnectedEventHandler">The change actual identifier to connected event handler.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        public SelectAdapterViewModel(ILogger logger, ILifetimeScope scope, IChangeActualIdToConnectedEventHandler changeActualIdToConnectedEventHandler, IGetActualNodeId getActualNodeId)
        {
            this.Logger = logger;
            this.Scope = scope;
            this.ChangeActualIdToConnectedEventHandler = changeActualIdToConnectedEventHandler;
            this.ConnectCommand = new RelayCommand(this.ConnectCommandAction);
            this.NodeIdChangeIsEnabled = true;

            this.NodeIdValue = getActualNodeId.Get();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the can adapter.
        /// </summary>
        /// <value>
        ///     The can adapter.
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

        /// <summary>
        /// Gets or sets a value indicating whether [node identifier change is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [node identifier change is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool NodeIdChangeIsEnabled
        {
            get => this.nodeIdChangeIsEnabled;
            set => this.SetProperty(ref this.nodeIdChangeIsEnabled, value);
        }

        /// <summary>
        ///     Gets or sets the can adapter.
        /// </summary>
        /// <value>
        ///     The can adapter.
        /// </value>
        public ushort NodeIdValue
        {
            get => this.nodeIdValue;
            set
            {
                this.ChangeActualIdToConnectedEventHandler.OnReached(value);
                this.SetProperty(ref this.nodeIdValue, value);
            }
        }

        private IChangeActualIdToConnectedEventHandler ChangeActualIdToConnectedEventHandler { get; }

        private IHandleInputs HandleInputs { get; set; }

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
                    case CanAdapter.None:
                        if (this.HandleInputs != null)
                        {
                            this.HandleInputs.Stop();
                            this.HandleInputs = null;
                            this.NodeIdChangeIsEnabled = true;
                        }

                        break;
                    case CanAdapter.PeakUsb:
                        this.HandleInputs = this.Scope.Resolve<IHandleInputs>();
                        this.HandleInputs.Start();
                        this.NodeIdChangeIsEnabled = false;
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