using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig.Analog
{
    using System.IO.Ports;
    using System.Threading;
    using System.Windows.Input;

    using Global.UiHelper;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class AnalogViewModel : BindableBase, IAnalogViewModel
    {
        public AnalogViewModel(ILogger logegr)
        {
            this.Logegr = logegr;
            this.OpenValueCommand = new RelayCommand(OpenValueCommandAction);
        }

        private ILogger Logegr { get; }

        private ICommand OpenValueCommand { get; }

        private string result;

        public string Result
        {
            get => this.result;
            set => this.SetProperty(ref this.result, value);
        }

        private SerialPort SerialPort { get; set; }

        private Timer Timer { get; set; }

        private void OpenValueCommandAction(object obj)
        {
            SerialPort = new SerialPort();
            this.SerialPort.BaudRate = 57600;
            this.SerialPort.PortName = "COM19";
            this.SerialPort.Open();


            this.Timer = new Timer(RequestCallback, null, 0, 250);

        }

        private void RequestCallback(object obj)
        {
            try
            {
                this.SerialPort.WriteLine("AnaCh1");

                Result = this.SerialPort.ReadLine();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
