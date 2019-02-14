namespace CPIOngConfig.Analog
{
    using System;
    using System.Collections.Generic;
    using System.IO.Ports;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The service for analog view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Analog.IAnalogViewModel" />
    public class AnalogViewModel : BindableBase, IAnalogViewModel
    {
        private static readonly object LockSerialWrite = new object();

        private bool analogValuePolling;

        private List<string> comPorts;

        private string console;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnalogViewModel" /> class.
        /// </summary>
        /// <param name="logegr">The logger.</param>
        public AnalogViewModel(ILogger logegr)
        {
            // todo mb: das ganze ding umbennen
            this.Logger = logegr;
            this.RefreshCommand = new RelayCommand(this.RefreshCommandAction);
            this.OpenValueCommand = new RelayCommand(this.OpenValueCommandAction);
            this.DisconnectCommand = new RelayCommand(this.DisconnectCommandAction);

            this.LoadComports();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether [analog value polling].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [analog value polling]; otherwise, <c>false</c>.
        /// </value>
        public bool AnalogValuePolling
        {
            get => this.analogValuePolling;
            set
            {
                if (value)
                {
                    this.StartPollingTask();
                }

                this.SetProperty(ref this.analogValuePolling, value);
            }
        }

        /// <summary>
        ///     Gets or sets the COM ports.
        /// </summary>
        /// <value>
        ///     The COM ports.
        /// </value>
        public List<string> ComPorts
        {
            get => this.comPorts;
            set => this.SetProperty(ref this.comPorts, value);
        }

        /// <summary>
        ///     Gets or sets the console.
        /// </summary>
        /// <value>
        ///     The console.
        /// </value>
        public string Console
        {
            get => this.console;
            set => this.SetProperty(ref this.console, value);
        }

        /// <summary>
        ///     Gets the disconnect command.
        /// </summary>
        /// <value>
        ///     The disconnect command.
        /// </value>
        public ICommand DisconnectCommand { get; }

        /// <summary>
        ///     Gets the open value command.
        /// </summary>
        /// <value>
        ///     The open value command.
        /// </value>
        public ICommand OpenValueCommand { get; }

        /// <summary>
        ///     Gets the refresh command.
        /// </summary>
        /// <value>
        ///     The refresh command.
        /// </value>
        public ICommand RefreshCommand { get; }

        private ILogger Logger { get; }

        private SerialPort SerialPort { get; set; }

        #endregion

        #region Private Methods

        private void LoadComports()
        {
            this.ComPorts = new List<string>();
            this.ComPorts = SerialPort.GetPortNames().ToList();
        }

        private void RefreshCommandAction(object obj)
        {
            try
            {
                this.LoadComports();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void DisconnectCommandAction(object obj)
        {
            try
            {
                this.SerialPort.Close();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void StartPollingTask()
        {
            Task.Run(
                () =>
                    {
                        var i = 0;
                        while (true)
                        {
                            lock (LockSerialWrite)
                            {
                                if (!this.SerialPort.IsOpen)
                                {
                                    break; // ma besten den task abbrechen und dings zurück setzen
                                }

                                this.SerialPort.DiscardInBuffer();

                                if (this.AnalogValuePolling)
                                {
                                    this.SerialPort.WriteLine($"AnaCh{i}");
                                }
                            }

                            Thread.Sleep(50);

                            ++i;

                            if (i > 15)
                            {
                                i = 0;
                            }
                        }
                    });
        }

        private void ListenTask()
        {
            Task.Run(
                () =>
                    {
                        while (true)
                        {
                            var res = string.Empty;
                            Thread.Sleep(50);

                            lock (LockSerialWrite)
                            {
                                try
                                {
                                    // todo mb: im read line muss mitgegebnen werden, was es ist
                                    res = this.SerialPort.ReadExisting(); // .ReadLine();
                                }
                                catch (TimeoutException)
                                {
                                    // ignore
                                }

                                this.Console += res;
                            }

                            // todo mb: analog werte antwort aufteilen
                        }
                    });
        }

        private void OpenValueCommandAction(object obj)
        {
            try
            {
                var comPort = (string)obj;

                if (string.IsNullOrEmpty(comPort))
                {
                    MessageBox.Show("Please select com port");
                    return;
                }

                this.SerialPort = new SerialPort();
                this.SerialPort.BaudRate = 57600;
                this.SerialPort.PortName = comPort;
                this.SerialPort.Close();
                this.SerialPort.Open();
                this.SerialPort.ReadTimeout = 200;
                this.ListenTask();
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