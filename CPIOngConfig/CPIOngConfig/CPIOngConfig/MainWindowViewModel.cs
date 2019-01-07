namespace CPIOngConfig
{
    using System;
    using System.Windows.Input;

    using Autofac;

    using CPIOngConfig.ConfigInputs;
    using CPIOngConfig.Contracts.ActiveSensor;
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.Analog;
    using CPIOngConfig.Contracts.ConfigId;
    using CPIOngConfig.InputBinary;
    using CPIOngConfig.Pulse;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The main window view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class MainWindowViewModel : BindableBase
    {
        private IAnalogView analogView;

        private IConfigCanId configCanId;

        private IConfigInputsAllView configInputsAllView;

        private IInputBinaryView inputBinaryView;

        private IPulseView pulseView;

        private ISelectAdapterView selectAdapterView;

        private IActiveSensorView activeSensorView;

        private IAliveView aliveView;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="scope">The scope.</param>
        public MainWindowViewModel(ILogger logger, ILifetimeScope scope)
        {
            this.Logger = logger;
            this.Scope = scope;

            this.WindowLoadCommand = new RelayCommand(this.WindowLoadCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the analog view.
        /// </summary>
        /// <value>
        ///     The analog view.
        /// </value>
        public IAnalogView AnalogView
        {
            get => this.analogView;
            private set => this.SetProperty(ref this.analogView, value);
        }

        /// <summary>
        ///     Gets the configuration can identifier.
        /// </summary>
        /// <value>
        ///     The configuration can identifier.
        /// </value>
        public IConfigCanId ConfigCanId
        {
            get => this.configCanId;
            private set => this.SetProperty(ref this.configCanId, value);
        }

        /// <summary>
        ///     Gets the configuration inputs all view.
        /// </summary>
        /// <value>
        ///     The configuration inputs all view.
        /// </value>
        public IConfigInputsAllView ConfigInputsAllView
        {
            get => this.configInputsAllView;
            private set => this.SetProperty(ref this.configInputsAllView, value);
        }

        /// <summary>
        ///     Gets the input binary view.
        /// </summary>
        /// <value>
        ///     The input binary view.
        /// </value>
        public IInputBinaryView InputBinaryView
        {
            get => this.inputBinaryView;
            private set => this.SetProperty(ref this.inputBinaryView, value);
        }

        /// <summary>
        ///     Gets the pulse view.
        /// </summary>
        /// <value>
        ///     The pulse view.
        /// </value>
        public IPulseView PulseView
        {
            get => this.pulseView;
            private set => this.SetProperty(ref this.pulseView, value);
        }

        /// <summary>
        ///     Gets the select adapter view.
        /// </summary>
        /// <value>
        ///     The select adapter view.
        /// </value>
        public ISelectAdapterView SelectAdapterView
        {
            get => this.selectAdapterView;
            private set => this.SetProperty(ref this.selectAdapterView, value);
        }

        /// <summary>
        /// Gets the active sensor view.
        /// </summary>
        /// <value>
        /// The active sensor view.
        /// </value>
        public IActiveSensorView ActiveSensorView
        {
            get => this.activeSensorView;
            private set => this.SetProperty(ref this.activeSensorView, value);
        }

        /// <summary>
        /// Gets the alive view.
        /// </summary>
        /// <value>
        /// The alive view.
        /// </value>
        public IAliveView AliveView
        {
            get => this.aliveView;
            private set => this.SetProperty(ref this.aliveView, value);
        }

        /// <summary>
        ///     Gets or sets the window load command.
        /// </summary>
        /// <value>
        ///     The window load command.
        /// </value>
        public ICommand WindowLoadCommand { get; set; }

        private ILogger Logger { get; }

        private ILifetimeScope Scope { get; }

        #endregion

        #region Private Methods

        private void WindowLoadCommandAction(object obj)
        {
            try
            {
                this.ConfigCanId = this.Scope.Resolve<IConfigCanId>();

                this.SelectAdapterView = this.Scope.Resolve<ISelectAdapterView>();
                this.InputBinaryView = this.Scope.Resolve<IInputBinaryView>();
                this.ConfigInputsAllView = this.Scope.Resolve<IConfigInputsAllView>();
                this.AnalogView = this.Scope.Resolve<IAnalogView>();
                this.PulseView = this.Scope.Resolve<IPulseView>();
                this.ActiveSensorView = this.Scope.Resolve<IActiveSensorView>();
                this.AliveView = this.Scope.Resolve<IAliveView>();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion
    }
}