namespace CPIOngConfig.Analog
{
    using System;
    using System.IO.Ports;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
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


        #endregion

        #region Private Methods

        private void OpenValueCommandAction(object obj)
        {
            this.SerialPort = new SerialPort();
            this.SerialPort.BaudRate = 57600;
            this.SerialPort.PortName = "COM19";
            this.SerialPort.Close();
            this.SerialPort.Open();
            this.SerialPort.ReadTimeout = 200;


            
           // this.SerialPort.WriteLine("AnaCh15\0");
            //this.SerialPort.Write(Encoding.ASCII.GetBytes("AnaCh15\0"), 0, 8);//"AnaCh15\0"

            Task.Run(
                () =>
                    {

                        while(true)
                        {
                            this.SerialPort.DiscardInBuffer();
                            this.SerialPort.WriteLine("AnaCh15");
                            string res = string.Empty;
                            Thread.Sleep(50);
                            // achtung endloss abbruch
                            //do
                            //{
                            try
                            {
                                res = this.SerialPort.ReadLine();
                            }
                            catch (TimeoutException ex)
                            {
                                ; // ignore
                            }

                            //}
                            //while (string.IsNullOrEmpty(res));

                            this.Result = res;
                        }
                      


                    });

            //this.SerialPort.Close();


            //this.Timer = new Timer(this.RequestCallback, null, 0, 100);
        }





        //private void RequestCallback(object obj)
        //{
        //    try
        //    {




        //        this.SerialPort.WriteLine("AnaCh15");

        //        string res = string.Empty;
        //        //Thread.Sleep(100);
        //        Thread.Sleep(10);
        //        // achtung endloss abbruch
        //        //do
        //        //{
        //        try
        //        {
        //            res = this.SerialPort.ReadLine();
        //        }
        //        catch (TimeoutException ex)
        //        {
        //            ; // ignore
        //        }

        //        //}
        //        //while (string.IsNullOrEmpty(res));

        //        this.Result = res;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        #endregion
    }
}