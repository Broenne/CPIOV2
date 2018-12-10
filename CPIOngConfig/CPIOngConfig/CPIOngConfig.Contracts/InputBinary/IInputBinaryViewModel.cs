using System.Collections.Generic;
using System.Windows.Input;

namespace CPIOngConfig.InputBinary
{
    public interface IInputBinaryViewModel
    {
        ICommand GetCommand { get; }
        List<InOutState> Input { get; set; }
    }
}