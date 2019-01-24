namespace CPIOngConfig.FlipFlop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using ConfigLogicLayer.DigitalInputState;

    using CPIOngConfig.Contracts.FlipFlop;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class FlipFlopViewModel : BindableBase, IFlipFlopViewModel
    {
        private readonly Dispatcher dispatcher = RootDispatcherFetcher.RootDispatcher;

        private ObservableCollection<bool> flipFlopState;

        #region Constructor

        public FlipFlopViewModel(IFlipFlopEventHandler flipFlopEventHandler, ILogger logger, IResetFlipFlop resetFlipFlop)
        {
            this.FlipFlopEventHandler = flipFlopEventHandler;
            this.Logger = logger;
            this.ResetFlipFlop = resetFlipFlop;

            var defaultList = new List<bool>();
            for (var i = 0; i < 16; i++)
            {
                defaultList.Add(i % 2 == 1);
            }

            this.FlipFlopState = new ObservableCollection<bool>(defaultList);

            this.FlipFlopEventHandler.EventIsReached += this.FlipFlopEventHandler_EventIsReached;

            this.ResetAllCommand = new RelayCommand(this.ResetAllCommandAction);
        }

        #endregion

        #region Properties

        public ObservableCollection<bool> FlipFlopState
        {
            get => this.flipFlopState;
            set => this.SetProperty(ref this.flipFlopState, value);
        }

        /// <summary>
        ///     Gets or sets the reset all command.
        /// </summary>
        /// <value>
        ///     The reset all command.
        /// </value>
        public ICommand ResetAllCommand { get; set; }

        private IFlipFlopEventHandler FlipFlopEventHandler { get; }

        private ILogger Logger { get; }

        private IResetFlipFlop ResetFlipFlop { get; }

        #endregion

        #region Private Methods

        private void ResetAllCommandAction(object obj)
        {
            try
            {
                this.ResetFlipFlop.ResetAll();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void FlipFlopEventHandler_EventIsReached(object sender, FlipFlopEventArgs e)
        {
            try
            {
                this.dispatcher.Invoke(
                    () =>
                        {
                            for (var i = 0; i < 16; ++i)
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