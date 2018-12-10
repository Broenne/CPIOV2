using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CPIOngConfig.InputBinary
{
    /// <summary>
    /// Interaction logic for InputBinaryView.xaml
    /// </summary>
    public partial class InputBinaryView : UserControl, IInputBinaryView
    {
        public InputBinaryView(IInputBinaryViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }
    }
}
