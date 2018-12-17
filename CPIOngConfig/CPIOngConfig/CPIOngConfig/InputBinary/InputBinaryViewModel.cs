using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig.InputBinary
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using ConfigLogicLayer.Contracts.DigitalInputState;
    using ConfigLogicLayer.DigitalInputState;

    using CPIOngConfig.Contracts.InputBinary;

    using Global.UiHelper;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class InputBinaryViewModel : BindableBase, IInputBinaryViewModel
    {
        private SolidColorBrush green;

        private SolidColorBrush gray;


        public InputBinaryViewModel(ILogger logger, IInputBinaryEventHandler inputBinaryEventHandler)
        {
            this.Logger = logger;
            //this.HandleInputs = handleInputs;
            this.GetCommand = new RelayCommand(this.GetCommandAction);

            this.gray = new SolidColorBrush(Colors.Gray);
            this.green = new SolidColorBrush(Colors.Green);

            this.Input = new List<InOutState>();
            for (int i = 0; i < 16; ++i)
            {
                this.Input.Add(new InOutState(0, this.gray));
            }

            inputBinaryEventHandler.EventIsReached += this.InputBinaryEventHandler_EventIsReached;
        }

        private void InputBinaryEventHandler_EventIsReached(object sender, InputBinaryEventArgs e)
        {
            try
            {
                int i = 0;
                foreach (var item in e.Store)
                {
                    this.Input[i].Color = item.Value ? this.green : this.gray;
                    ++i;
                }
                
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        public List<InOutState> Input { get; set; }


        private void GetCommandAction(object obj)
        {
            try
            {
                //var res = this.HandleInputs.Get(4);

                //for (int i=0;i< res.Count; i++)
                //{
                //    

                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private ILogger Logger { get; }

        public ICommand GetCommand { get; }


    }
}
