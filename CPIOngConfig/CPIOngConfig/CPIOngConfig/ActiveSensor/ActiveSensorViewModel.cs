namespace CPIOngConfig.ActiveSensor
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.ActiveSensor;
    using CPIOngConfig.Contracts.ConfigInputs;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The view model for set active sensor.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.ActiveSensor.IActiveSensorViewModel" />
    public class ActiveSensorViewModel : BindableBase, IActiveSensorViewModel
    {
        private Modi selctedValue;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActiveSensorViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="setActiveSensorToDetect">The set active sensor to detect.</param>
        /// <param name="activeSensorEventHandler">The active sensor event handler.</param>
        public ActiveSensorViewModel(ILogger logger, ISetActiveSensorToDetect setActiveSensorToDetect, IActiveSensorEventHandler activeSensorEventHandler)
        {
            this.Logger = logger;
            this.SetActiveSensorToDetect = setActiveSensorToDetect;
            this.ActiveSensorEventHandler = activeSensorEventHandler;

            this.ActiveSensorEventHandler.EventIsReached += this.ActiveSensorEventHandler_EventIsReached;
            this.SetSensorCommand = new RelayCommand(this.SetSensorCommandAction);
            this.LoadActiveSensorCommand = new RelayCommand(this.LoadActiveSensorCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the load active sensor command.
        /// </summary>
        /// <value>
        ///     The load active sensor command.
        /// </value>
        public ICommand LoadActiveSensorCommand { get; }

        /// <summary>
        /// Gets or sets the selected value.
        /// </summary>
        /// <value>
        /// The selected value.
        /// </value>
        public Modi SelctedValue
        {
            get => this.selctedValue;

            set => this.SetProperty(ref this.selctedValue, value);
        }

        /// <summary>
        ///     Gets the set sensor command.
        /// </summary>
        /// <value>
        ///     The set sensor command.
        /// </value>
        public ICommand SetSensorCommand { get; }

        private IActiveSensorEventHandler ActiveSensorEventHandler { get; }

        private ILogger Logger { get; }

        private ISetActiveSensorToDetect SetActiveSensorToDetect { get; }

        #endregion

        #region Private Methods

        private void ActiveSensorEventHandler_EventIsReached(object sender, Modi e)
        {
            try
            {
                this.SelctedValue = e;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadActiveSensorCommandAction(object obj)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                // trigger command
                this.SetActiveSensorToDetect.TriggerActiveSensorState();
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

        private void SetSensorCommandAction(object obj)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                var modi = (Modi)obj;
                this.SetActiveSensorToDetect.Do(modi);
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

        #endregion
    }
}