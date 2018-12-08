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

namespace CPIOngConfig
{
    using CPIOngConfig.Contracts;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mainWindowViewModel">The main window view model.</param>
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            // todo mb: das muss auto rein
            try
            {
                this.InitializeComponent();
                this.DataContext = mainWindowViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Private Methods

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            // todo mb
        }

        #endregion
    }
}
