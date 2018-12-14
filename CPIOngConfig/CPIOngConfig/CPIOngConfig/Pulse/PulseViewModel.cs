namespace CPIOngConfig.Pulse
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Threading;

    using CPIOngConfig.Contracts.Pulse;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="IPulseViewModel" />
    public class PulseViewModel : BindableBase, IPulseViewModel
    {
        public List<PulseDataForView> pulseDataForViewList;

        private readonly Dispatcher dispatcher = RootDispatcherFetcher.RootDispatcher;

        private Timer TimerTest;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PulseViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PulseViewModel(ILogger logger, IPulseEventHandler pulseEventHandler)
        {
            this.Logger = logger;
            this.PulseDataForViewList = new List<PulseDataForView>();

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

        //void Test(object state)
        //{
        //    var rand = new Random();
        //    for (int i = 0; i < 16; i++)
        //    {

        //        dispatcher.Invoke(() => { this.PulseDataForViewList[i].AddTime((uint)(rand.Next(0, 100))); });

        //    }

        //}
    }
}