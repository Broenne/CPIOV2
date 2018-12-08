using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CPIOngConfig
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Gets the OSCI BOOTSTRAPPER.
        /// </summary>
        /// <value>
        /// The OSCI BOOTSTRAPPER.
        /// </value>
        public ConfigBootstrapper ConfigBootstrapper { get; private set; }

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                this.ConfigBootstrapper = new ConfigBootstrapper();
                this.ConfigBootstrapper.Run();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //throw;
            }
        }

        #endregion

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            this.ConfigBootstrapper.Dispose(); // todo mb: store values
        }
    }
}
