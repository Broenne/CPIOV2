namespace CPIOngConfig.FactorPulse
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using CPIOngConfig.Contracts.FactorPulse;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The factor pulse view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.FactorPulse.IFactorPulseViewModel" />
    public class FactorPulseViewModel : BindableBase, IFactorPulseViewModel
    {
        private uint pulsPerRevolution;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FactorPulseViewModel"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public FactorPulseViewModel(ILogger logger)
        {
            this.Logger = logger;
            this.CheckBoxChangeCommand = new RelayCommand(this.CheckBoxChangeCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CheckBox change command.
        /// </summary>
        /// <value>
        /// The CheckBox change command.
        /// </value>
        public ICommand CheckBoxChangeCommand { get; set; }

        /// <summary>
        ///     Gets or sets the pulse data for view list.
        /// </summary>
        /// <value>
        ///     The pulse data for view list.
        /// </value>
        public uint PulsPerRevolution
        {
            get => this.pulsPerRevolution;
            set => this.SetProperty(ref this.pulsPerRevolution, value);
        }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void CheckBoxChangeCommandAction(object obj)
        {
            try
            {
                var isChecked = (bool)obj;
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