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

        private DestinationTimeBase destinationTimeBase = DestinationTimeBase.Raw;

        private uint pulsPerRevolution;

        private string selectedTimeBase;

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

            this.TimeBase = new ObservableCollection<string>(new List<string> { Millisecond, Second, Minute, Hour });
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
        /// Gets or sets the selected time base.
        /// </summary>
        /// <value>
        /// The selected time base.
        /// </value>
        public string SelectedTimeBase
        {
            get => this.selectedTimeBase;
            set
            {
                var unit = value;

                this.destinationTimeBase = DestinationTimeBase.Raw;
                if (unit.Equals(Hour))
                {
                    this.destinationTimeBase = DestinationTimeBase.Hour;
                }

                if (unit.Equals(Minute))
                {
                    this.destinationTimeBase = DestinationTimeBase.Minute;
                }

                if (unit.Equals(Second))
                {
                    this.destinationTimeBase = DestinationTimeBase.Second;
                }

                if (unit.Equals(Millisecond))
                {
                    this.destinationTimeBase = DestinationTimeBase.Millisecond;
                }

                this.SetProperty(ref this.selectedTimeBase, value);
            }
        }

        /// <summary>
        /// Gets or sets the time base.
        /// </summary>
        /// <value>
        /// The time base.
        /// </value>
        public ObservableCollection<string> TimeBase { get; set; }

        /// <summary>
        ///     Gets or sets the unit change command.
        /// </summary>
        /// <value>
        ///     The unit change command.
        /// </value>
        public ICommand UnitChangeCommand { get; set; }

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
                var destinationTimeBaseInternal = DestinationTimeBase.Raw;
                if (isChecked)
                {
                    volumePerTimeSlot = this.VolumePerRevolution / this.PulsPerRevolution;
                    destinationTimeBaseInternal = this.destinationTimeBase;
                }

                var factorPulseEventArgs = new FactorPulseEventArgs(destinationTimeBaseInternal, volumePerTimeSlot);
                this.FactorPulseEventHandler.OnReached(factorPulseEventArgs);
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