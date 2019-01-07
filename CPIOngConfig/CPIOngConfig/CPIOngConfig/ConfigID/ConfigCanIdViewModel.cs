namespace CPIOngConfig.ConfigID
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.ConfigId;

    using Helper;
    using Helper.Contracts.Logger;

    /// <summary>
    ///     The config can id view model.
    /// </summary>
    public class ConfigCanIdViewModel : IConfigCanIdViewModel
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigCanIdViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="setCanIdonDevice">The set can id on device.</param>
        public ConfigCanIdViewModel(ILogger logger, ISetCanIdOnDevice setCanIdonDevice)
        {
            this.Logger = logger;
            this.SetCanIdonDevice = setCanIdonDevice;
            this.SaveIdCommand = new RelayCommand(this.SaveIdCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the save identifier command.
        /// </summary>
        /// <value>
        /// The save identifier command.
        /// </value>
        public ICommand SaveIdCommand { get; }

        private ILogger Logger { get; }

        private ISetCanIdOnDevice SetCanIdonDevice { get; }

        #endregion

        #region Private Methods

        private void SaveIdCommandAction(object obj)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                var id = Convert.ToByte(obj);
                this.SetCanIdonDevice.Do(id);
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