namespace CPIOngConfig.Pulse
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Threading;

    using CPIOngConfig.Contracts.FactorPulse;
    using CPIOngConfig.Contracts.Pulse;

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

        private double factor = 1;

        private List<PulseDataForView> pulseDataForViewList;

        private IFactorPulseView pulseFactorView;

        private IPulseStorageView pulseStorageView;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pulseEventHandler">The pulse event handler.</param>
        /// <param name="pulseStorageView">The pulse storage view.</param>
        /// <param name="factorPulseViewArg">The factor pulse view argument.</param>
        /// <param name="factorPulseEventHandler">The factor pulse event handler.</param>
        public PulseViewModel(ILogger logger, IPulseEventHandler pulseEventHandler, IPulseStorageView pulseStorageView, IFactorPulseView factorPulseViewArg, IFactorPulseEventHandler factorPulseEventHandler)
        {
            this.Logger = logger;
            this.PulseDataForViewList = new List<PulseDataForView>();
            this.PulseStorageView = pulseStorageView;
            this.PulseFactorView = factorPulseViewArg;
            factorPulseEventHandler.EventIsReached += this.FactorPulseEventHandler_EventIsReached;
            

            // todo mb: parallel for
            for (var i = 0; i < 16; i++)
            {
                var pulseDataForView = new PulseDataForView($"{i}", 100);
                pulseDataForView.AddTime(0);
                this.PulseDataForViewList.Add(pulseDataForView);
            }

            pulseEventHandler.EventIsReached += this.PulseEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

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
        ///     Gets or sets the pulse factor view.
        /// </summary>
        /// <value>
        ///     The pulse factor view.
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

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void FactorPulseEventHandler_EventIsReached(object sender, double e)
        {
            try
            {
                this.factor = e;
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
                // aktuell t in 0,1 ms
                var timDifin0D1Ms = e.Stamp;

                var timDifInMs = timDifin0D1Ms / 10.0;
                var timeInSecond = timDifInMs / 1000.0;
                var tinmeInMinutes = timeInSecond / 60;


                // Q = V / t
                double valueToList = this.factor * /*timDifin0D1Ms*/timeInSecond;


                this.dispatcher.Invoke(() => { this.PulseDataForViewList[e.Channel].AddTime(valueToList); });
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