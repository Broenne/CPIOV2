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

        private double volumePerRevolution;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="FactorPulseViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="factorPulseEventHandler">The factor pulse event handler.</param>
        public FactorPulseViewModel(ILogger logger, IFactorPulseEventHandler factorPulseEventHandler)
        {
            this.Logger = logger;
            this.FactorPulseEventHandler = factorPulseEventHandler;
            this.CheckBoxChangeCommand = new RelayCommand(this.CheckBoxChangeCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the CheckBox change command.
        /// </summary>
        /// <value>
        ///     The CheckBox change command.
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

        /// <summary>
        ///     Gets or sets the volume per revolution.
        /// </summary>
        /// <value>
        ///     The volume per revolution.
        /// </value>
        public double VolumePerRevolution
        {
            get => this.volumePerRevolution;
            set => this.SetProperty(ref this.volumePerRevolution, value);
        }

        private IFactorPulseEventHandler FactorPulseEventHandler { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void CheckBoxChangeCommandAction(object obj)
        {
            try
            {
                var isChecked = (bool)obj;

                double volumePerTimeSlot = 1;

                if (isChecked)
                {
                    volumePerTimeSlot = this.VolumePerRevolution / this.PulsPerRevolution;
                }

                this.FactorPulseEventHandler.OnReached(volumePerTimeSlot);
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