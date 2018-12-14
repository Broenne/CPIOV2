using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig.InputBinary
{
    using System.Windows.Input;
    using System.Windows.Media;

    using ConfigLogicLayer.Contracts.DigitalInputState;
    using ConfigLogicLayer.DigitalInputState;

    using Global.UiHelper;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class InputBinaryViewModel : BindableBase, IInputBinaryViewModel
    {
        private SolidColorBrush green;

        private SolidColorBrush gray;


        public InputBinaryViewModel(ILogger logger, IHandleInputs handleInputs)
        {
            this.Logger = logger;
            this.HandleInputs = handleInputs;
            this.GetCommand = new RelayCommand(this.GetCommandAction);

            this.gray = new SolidColorBrush(Colors.Gray);
            this.green = new SolidColorBrush(Colors.Green);

            this.Input = new List<InOutState>();
            for (int i = 0; i < 16; ++i)
            {
                this.Input.Add(new InOutState(0, this.gray));
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
                //    this.Input[i].Color = res[i] ? this.green:this.gray;

                //}



            }
            catch (Exception ex)
            {
                ;
            }

        }

        private ILogger Logger { get; }

        public ICommand GetCommand { get; }

        private IHandleInputs HandleInputs { get; }

    }
}
