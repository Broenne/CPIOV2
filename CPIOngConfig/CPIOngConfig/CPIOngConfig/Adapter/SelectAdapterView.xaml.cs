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

namespace CPIOngConfig.Adapter
{
    using CPIOngConfig.Contracts.Adapter;

    /// <summary>
    /// Interaction logic for SelectAdapterView.xaml
    /// </summary>
    public partial class SelectAdapterView : UserControl, ISelectAdapterView
    {
        public SelectAdapterView(ISelectAdapterViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
