namespace CPIOngConfig.FactorPulse
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        #region Constants

        private const string Hour = "h";

        private const string Millisecond = "ms";

        private const string Minute = "m";

        private const string Second = "s";

        #endregion

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
            this.UnitChangeCommand = new RelayCommand(this.UnitChangeCommandAction);

            this.TimeBase = new ObservableCollection<string>(new List<string> { Millisecond, Second, Minute, Hour });

        }

        #endregion

        #region Properties

        public ICommand UnitChangeCommand { get; set; }

        private void UnitChangeCommandAction(object obj)
        {
            try
            {
                ;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

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

        public ObservableCollection<string> TimeBase { get; set; }

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