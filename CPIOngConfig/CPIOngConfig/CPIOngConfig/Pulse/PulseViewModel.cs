namespace CPIOngConfig.Pulse
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Threading;

    using CPIOngConfig.Contracts.Pulse;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    /// The pulse view model.
    /// </summary>
    /// <seealso cref="CPIOngConfig.Contracts.Pulse.IPulseViewModel" />
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="IPulseViewModel" />
    public class PulseViewModel : BindableBase, IPulseViewModel
    {
        private readonly Dispatcher dispatcher = RootDispatcherFetcher.RootDispatcher;

        private List<PulseDataForView> pulseDataForViewList;

        private IPulseStorageView pulseStorageView;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pulseEventHandler">The pulse event handler.</param>
        /// <param name="pulseStorageView">The pulse storage view.</param>
        public PulseViewModel(ILogger logger, IPulseEventHandler pulseEventHandler, IPulseStorageView pulseStorageView)
        {
            this.Logger = logger;
            this.PulseDataForViewList = new List<PulseDataForView>();
            this.PulseStorageView = pulseStorageView;

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
        /// Gets or sets the pulse data for view list.
        /// </summary>
        /// <value>
        /// The pulse data for view list.
        /// </value>
        public IPulseStorageView PulseStorageView
        {
            get => this.pulseStorageView;
            set => this.SetProperty(ref this.pulseStorageView, value);
        }

        /// <summary>
        /// Gets or sets the pulse data for view list.
        /// </summary>
        /// <value>
        /// The pulse data for view list.
        /// </value>
        public List<PulseDataForView> PulseDataForViewList
        {
            get => this.pulseDataForViewList;
            set => this.SetProperty(ref this.pulseDataForViewList, value);
        }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void PulseEventHandler_EventIsReached(object sender, PulseEventArgs e)
        {
            try
            {
                this.dispatcher.Invoke(() => { this.PulseDataForViewList[e.Channel].AddTime(e.Stamp); });
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