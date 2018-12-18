namespace CPIOngConfig.ActiveSensor
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using CPIOngConfig.Contracts.ActiveSensor;

    using Global.UiHelper;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The view model for set active sensor.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.ActiveSensor.IActiveSensorViewModel" />
    public class ActiveSensorViewModel : BindableBase, IActiveSensorViewModel
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActiveSensorViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ActiveSensorViewModel(ILogger logger)
        {
            this.Logger = logger;
            this.SetSensorCommand = new RelayCommand(this.SetSensorCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the set sensor command.
        /// </summary>
        /// <value>
        /// The set sensor command.
        /// </value>
        public ICommand SetSensorCommand { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void SetSensorCommandAction(object obj)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
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