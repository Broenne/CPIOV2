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

namespace CPIOngConfig.ConfigID
{
    using global::CPIOngConfig.Contracts.ConfigId;

    /// <summary>
    /// Interaction logic for ConfigCanId.xaml
    /// </summary>
    public partial class ConfigCanId : UserControl, IConfigCanId
    {
        public ConfigCanId()
        {
            InitializeComponent();
        }
    }
}
