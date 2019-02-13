namespace CPIOngConfig.Error
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.CanText;
    using CPIOngConfig.Contracts.Error;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The error hardware view model.
    /// </summary>
    /// <seealso cref="CPIOngConfig.Contracts.Error.IErrorHardwareViewModel" />
    public class ErrorHardwareViewModel : BindableBase, IErrorHardwareViewModel
    {
        private readonly SolidColorBrush green = new SolidColorBrush(Colors.Green);

        private readonly SolidColorBrush red = new SolidColorBrush(Colors.Red);

        private ObservableCollection<SolidColorBrush> color;

        private bool isEnabled;

        private string rawErrorFildData;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHardwareViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="aliveEventHandler">The alive event handler.</param>
        /// <param name="canInfoTextView">The can information text view.</param>
        /// <param name="activateDebugMode">The activate debug mode.</param>
        /// <param name="canIsConnectedEventHandler">The can is connected event handler.</param>
        public ErrorHardwareViewModel(ILogger logger, IAliveEventHandler aliveEventHandler, ICanInfoTextView canInfoTextView, IActivateDebugMode activateDebugMode, ICanIsConnectedEventHandler canIsConnectedEventHandler)
        {
            this.Logger = logger;
            this.AliveEventHandler = aliveEventHandler;
            this.CanInfoTextView = canInfoTextView;
            this.ActivateDebugMode = activateDebugMode;

            canIsConnectedEventHandler.EventIsReached += this.CanIsConnectedEventHandler_EventIsReached;

            this.Color = new ObservableCollection<SolidColorBrush>();

            for (var i = 0; i < CanCommandConsts.CountOfErrorBytes * 8; i++)
            {
                this.Color.Add(this.red);
            }

            this.AliveEventHandler.EventIsReached += this.AliveEventHandler_EventIsReached;
            this.ActivateCommand = new RelayCommand(this.ActivateCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the activate command.
        /// </summary>
        /// <value>
        ///     The activate command.
        /// </value>
        public ICommand ActivateCommand { get; }

        /// <summary>
        ///     Gets the can information text view.
        /// </summary>
        /// <value>
        ///     The can information text view.
        /// </value>
        public ICanInfoTextView CanInfoTextView { get; }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public ObservableCollection<SolidColorBrush> Color
        {
            get => this.color;

            set => this.SetProperty(ref this.color, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c>If this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get => this.isEnabled;

            set => this.SetProperty(ref this.isEnabled, value);
        }

        /// <summary>
        ///     Gets or sets the raw error fill data.
        /// </summary>
        /// <value>
        ///     The raw error fill data.
        /// </value>
        public string RawErrorFildData
        {
            get => this.rawErrorFildData;
            set => this.SetProperty(ref this.rawErrorFildData, value);
        }

        private IActivateDebugMode ActivateDebugMode { get; }

        private IAliveEventHandler AliveEventHandler { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void CanIsConnectedEventHandler_EventIsReached(object sender, bool e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                this.IsEnabled = e;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        private void ActivateCommandAction(object obj)
        {
            try
            {
                if ((bool)obj)
                {
                    this.ActivateDebugMode.Activate();
                }
                else
                {
                    this.ActivateDebugMode.Deactivate();
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void AliveEventHandler_EventIsReached(object sender, AliveEventArgs e)
        {
            try
            {
                this.Color[0] = this.green;

                for (var i = 0; i < 32; ++i)
                {
                    var hasError = e.Errors[i / 8] << (i % 8) == 1;

                    this.Color[i] = hasError ? this.green : this.red;
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