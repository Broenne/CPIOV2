namespace CPIOngConfig.Pulse
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using CPIOngConfig.Contracts.FactorPulse;
    using CPIOngConfig.Contracts.Pulse;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The pulse view model.
    /// </summary>
    /// <seealso cref="CPIOngConfig.Contracts.Pulse.IPulseViewModel" />
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="IPulseViewModel" />
    public class PulseViewModel : BindableBase, IPulseViewModel
    {
        private readonly Dispatcher dispatcher = RootDispatcherFetcher.RootDispatcher;

        private List<PulseDataForView> pulseDataForViewList;

        private IFactorPulseView pulseFactorView;

        private IPulseStorageView pulseStorageView;

        private double timefactor = 1;

        private double volumePerTimeSlot = 1;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PulseViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pulseEventHandler">The pulse event handler.</param>
        /// <param name="pulseStorageView">The pulse storage view.</param>
        /// <param name="factorPulseViewArg">The time factor pulse view argument.</param>
        /// <param name="factorPulseEventHandler">The time factor pulse event handler.</param>
        public PulseViewModel(ILogger logger, IPulseEventHandler pulseEventHandler, IPulseStorageView pulseStorageView, IFactorPulseView factorPulseViewArg, IFactorPulseEventHandler factorPulseEventHandler)
        {
            this.Logger = logger;
            this.PulseDataForViewList = new List<PulseDataForView>();
            this.PulseStorageView = pulseStorageView;
            this.PulseFactorView = factorPulseViewArg;

            this.ActivateCheckSumcCommand = new RelayCommand(this.ActivateCheckSumcCommandAction);
            this.ClearAllDataCommand = new RelayCommand(this.ClearAllDataCommandAction);

            // todo mb: parallel for
            for (var i = 0; i < 16; i++)
            {
                var pulseDataForView = new PulseDataForView($"{i}", 100, true);
                pulseDataForView.AddTime(0, 0, 0);
                this.PulseDataForViewList.Add(pulseDataForView);
            }

            this.ActivateCheckSumcCommandAction(true); // cativiert und füllt das feld

            factorPulseEventHandler.EventIsReached += this.FactorPulseEventHandler_EventIsReached;
            pulseEventHandler.EventIsReached += this.PulseEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the activate check sum command.
        /// </summary>
        /// <value>
        /// The activate check sum command.
        /// </value>
        public ICommand ActivateCheckSumcCommand { get; }

        public ICommand ClearAllDataCommand { get; }

        private void ClearAllDataCommandAction(object obj)
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
        ///     Gets or sets the pulse data for view list.
        /// </summary>
        /// <value>
        ///     The pulse data for view list.
        /// </value>
        public List<PulseDataForView> PulseDataForViewList
        {
            get => this.pulseDataForViewList;
            set => this.SetProperty(ref this.pulseDataForViewList, value);
        }

        /// <summary>
        ///     Gets or sets the pulse time factor view.
        /// </summary>
        /// <value>
        ///     The pulse time factor view.
        /// </value>
        public IFactorPulseView PulseFactorView
        {
            get => this.pulseFactorView;
            set => this.SetProperty(ref this.pulseFactorView, value);
        }

        /// <summary>
        ///     Gets or sets the pulse data for view list.
        /// </summary>
        /// <value>
        ///     The pulse data for view list.
        /// </value>
        public IPulseStorageView PulseStorageView
        {
            get => this.pulseStorageView;
            set => this.SetProperty(ref this.pulseStorageView, value);
        }

        private bool CheckSumIsActivated { get; set; }

        private List<CheckSumData> CheckSumStorage { get; set; } // = new List<CheckSumData>(new CheckSumData[16]);

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void ActivateCheckSumcCommandAction(object obj)
        {
            try
            {
                this.CheckSumIsActivated = (bool)obj;

                if (this.CheckSumIsActivated)
                {
                    this.CheckSumStorage = new List<CheckSumData>();
                    for (var i = 0; i < 16; i++)
                    {
                        this.CheckSumStorage.Add(new CheckSumData());
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void FactorPulseEventHandler_EventIsReached(object sender, FactorPulseEventArgs e)
        {
            try
            {
                this.timefactor = e.GetFactor();
                this.volumePerTimeSlot = e.VolumePerTimeSlot;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }
        
        private void PulseEventHandler_EventIsReached(object sender, PulseEventArgs e)
        {
            try
            {
                // Q = V / t
                double time = this.timefactor * e.Stamp;

                var flowCalculation = this.volumePerTimeSlot / time;

                var check = e.CheckSum;
                if (this.CheckSumIsActivated)
                {
                    this.CheckIfNextIsNext(e.Channel, e.CheckSum);
                }

                this.dispatcher.Invoke(() => { this.PulseDataForViewList[e.Channel].AddTime(e.Stamp, flowCalculation, check); });
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        private void CheckIfNextIsNext(int channel, byte checkSum)
        {
            // todo mb: was passietr beim ersten mal? wie initailisieren????
            string info = string.Empty;
            if (!this.CheckSumStorage[channel].Check(channel, checkSum, ref info))
            {
                MessageBox.Show(info);
            }

            //this.CheckSumStorage[channel] = new CheckSumData(checkSum);
            this.CheckSumStorage[channel].ChangeCheckSum(checkSum);
        }

        #endregion
    }
}