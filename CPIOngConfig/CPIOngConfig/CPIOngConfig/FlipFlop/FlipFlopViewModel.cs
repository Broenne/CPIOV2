namespace CPIOngConfig.FlipFlop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using CPIOngConfig.Contracts.FlipFlop;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    /// The flip flop view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.FlipFlop.IFlipFlopViewModel" />
    public class FlipFlopViewModel : BindableBase, IFlipFlopViewModel
    {
        private readonly Dispatcher dispatcher = RootDispatcherFetcher.RootDispatcher;

        private ObservableCollection<bool> flipFlopState;

        private ObservableCollection<bool> stateQmin;

        private ObservableCollection<bool> stateQmax;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FlipFlopViewModel"/> class.
        /// </summary>
        /// <param name="flipFlopEventHandler">The flip flop event handler.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="resetFlipFlop">The reset flip flop.</param>
        public FlipFlopViewModel(IFlipFlopEventHandler flipFlopEventHandler, ILogger logger, IResetFlipFlop resetFlipFlop)
        {
            this.FlipFlopEventHandler = flipFlopEventHandler;
            this.Logger = logger;
            this.ResetFlipFlop = resetFlipFlop;

            //var defaultList = new List<bool>();
            //for (var i = 0; i < 16; i++)
            //{
            //    defaultList.Add(false);
            //}

            //this.FlipFlopState = new ObservableCollection<bool>(defaultList);


            var defaultList = new List<bool>();
            for (var i = 0; i < 3; i++)
            {
                defaultList.Add(false);
            }

            this.StateQmax = new ObservableCollection<bool>(defaultList);

            defaultList = new List<bool>();
            for (var i = 0; i < 3; i++)
            {
                defaultList.Add(false);
            }

            this.StateQmin = new ObservableCollection<bool>(defaultList);


            this.FlipFlopEventHandler.EventIsReached += this.FlipFlopEventHandler_EventIsReached;

            this.ResetAllCommand = new RelayCommand(this.ResetAllCommandAction);
            this.ResetSingleCommand = new RelayCommand(this.ResetSingleCommandAction);
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the state of the flip flop.
        /// </summary>
        /// <value>
        /// The state of the flip flop.
        /// </value>
        public ObservableCollection<bool> StateQmin
        {
            get => this.stateQmin;
            set => this.SetProperty(ref this.stateQmin, value);
        }


        /// <summary>
        /// Gets or sets the state of the flip flop.
        /// </summary>
        /// <value>
        /// The state of the flip flop.
        /// </value>
        public ObservableCollection<bool> StateQmax
        {
            get => this.stateQmax;
            set => this.SetProperty(ref this.stateQmax, value);
        }

        /// <summary>
        /// Gets or sets the state of the flip flop.
        /// </summary>
        /// <value>
        /// The state of the flip flop.
        /// </value>
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

        /// <summary>
        /// Gets or sets the reset single command.
        /// </summary>
        /// <value>
        /// The reset single command.
        /// </value>
        public ICommand ResetSingleCommand { get; set; }

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

        private void ResetSingleCommandAction(object obj)
        {
            try
            {
                uint channel = Convert.ToUInt32(obj);
                this.ResetFlipFlop.ResetChannel(channel);
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
                                //this.FlipFlopState[i] = false;
                                var mask = 1 << (i % 8);
                                var actBit = e.RawData[i / 8] & mask;
                                bool stateofThis = actBit > 0;
                                switch (i)
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        this.StateQmin[i] = stateofThis;
                                        break;
                                    case 8:
                                    case 9:
                                    case 10:
                                        this.StateQmax[i % 8] = stateofThis;
                                        break;
                                    default:
                                        break;
                                            //throw new Exception("Faslcher Werit im Flip flop Register?! zu vielel!?");
                                }

                                //if (actBit > 0 && actBit < 8)
                                //{
                                //    this.FlipFlopState[i] = true;
                                //}
                                //else if(actBit > 0 && actBit < 8)
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