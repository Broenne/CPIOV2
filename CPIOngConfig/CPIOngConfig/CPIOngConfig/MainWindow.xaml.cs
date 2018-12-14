namespace CPIOngConfig
{
    using System;
    using System.Windows;

    using CPIOngConfig.Contracts;

    /// <summary>
    ///     Interaction logic for MainWindow.
    /// </summary>
    public partial class MainWindow : IMainWindow
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
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

        #region Private Methods

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            // todo mb
        }

        #endregion
    }
}