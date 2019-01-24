namespace CPIOngConfig.FlipFlop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using CPIOngConfig.Contracts.FlipFlop;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class FlipFlopViewModel : BindableBase, IFlipFlopViewModel
    {

        private readonly Dispatcher dispatcher = RootDispatcherFetcher.RootDispatcher;

        private ObservableCollection<bool> flipFlopState;

        #region Constructor

        public FlipFlopViewModel(IFlipFlopEventHandler flipFlopEventHandler, ILogger logger)
        {
            this.FlipFlopEventHandler = flipFlopEventHandler;
            this.Logger = logger;

            var defaultList = new List<bool>();
            for (var i = 0; i < 16; i++)
            {
                defaultList.Add(i % 2 == 1);
            }

            this.FlipFlopState = new ObservableCollection<bool>(defaultList);

            this.FlipFlopEventHandler.EventIsReached += this.FlipFlopEventHandler_EventIsReached;

            ResetAllCommand = new RelayCommand(this.ResetAllCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the reset all command.
        /// </summary>
        /// <value>
        /// The reset all command.
        /// </value>
        public ICommand ResetAllCommand { get; set; }

        private void ResetAllCommandAction(object obj)
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


        public ObservableCollection<bool> FlipFlopState
        {
            get => this.flipFlopState;
            set => this.SetProperty(ref this.flipFlopState, value);
        }

        private IFlipFlopEventHandler FlipFlopEventHandler { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void FlipFlopEventHandler_EventIsReached(object sender, FlipFlopEventArgs e)
        {
            try
            {

                this.dispatcher.Invoke(() =>
                    {
                        for (int i = 0; i < 16; ++i)
                        {
                            this.FlipFlopState[i] = false;
                            var mask = 1 << (i % 8);
                            var actBit = e.RawData[i / 8] & mask;
                            if (actBit > 0)
                            {
                                this.FlipFlopState[i] = true;
                            }
                        }
                    });
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