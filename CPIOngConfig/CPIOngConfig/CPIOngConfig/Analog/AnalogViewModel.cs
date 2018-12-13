namespace CPIOngConfig.Analog
{
    using System;
    using System.IO.Ports;
    using System.Threading;
    using System.Windows.Input;

    using Global.UiHelper;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class AnalogViewModel : BindableBase, IAnalogViewModel
    {
        private string result;

        #region Constructor

        public AnalogViewModel(ILogger logegr)
        {
            this.Logegr = logegr;
            this.OpenValueCommand = new RelayCommand(this.OpenValueCommandAction);
        }

        #endregion

        #region Properties

        public ICommand OpenValueCommand { get; }

        public string Result
        {
            get => this.result;
            set => this.SetProperty(ref this.result, value);
        }

        private ILogger Logegr { get; }

        private SerialPort SerialPort { get; set; }

        private Timer Timer { get; set; }

        #endregion

        #region Private Methods

        private void OpenValueCommandAction(object obj)
        {
            this.SerialPort = new SerialPort();
            this.SerialPort.BaudRate = 57600;
            this.SerialPort.PortName = "COM19";
            this.SerialPort.Open();


            this.SerialPort.WriteLine("AnaCh15\0");


            Thread.Sleep(100);

            string res = string.Empty;
            // achtung endloss abbruch
            //do
            //{
            res = this.SerialPort.ReadLine();
            //}
            //while (string.IsNullOrEmpty(res));

            this.Result = res;

            this.SerialPort.Close();


            //this.Timer = new Timer(this.RequestCallback, null, 0, 2500);
        }

        private void RequestCallback(object obj)
        {
            try
            {
                this.SerialPort.WriteLine("AnaCh1\0");


                Thread.Sleep(100);

                string res = string.Empty;
                // achtung endloss abbruch
                //do
                //{
                    res = this.SerialPort.ReadLine();
                //}
                //while (string.IsNullOrEmpty(res));

                this.Result = res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}