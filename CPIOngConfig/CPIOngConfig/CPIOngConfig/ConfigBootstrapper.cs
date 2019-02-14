namespace CPIOngConfig
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Threading;

    using Autofac;

    using ConfigLogicLayer;

    using CPIOngConfig.Contracts;

    using Hal.PeakCan;

    using Helper.Contracts.Logger;

    using Prism.Autofac;
    using Prism.Logging;

    /// <summary>
    ///     The OSCI BOOTSTRAPPER.
    /// </summary>
    /// <seealso cref="AutofacBootstrapper" />
    public class ConfigBootstrapper : AutofacBootstrapper, IDisposable
    {
        #region Properties

        private new ILogger Logger { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Container.Dispose();
        }

        /// <summary>
        ///     Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    this.Logger.LogDebug(message);
                    break;
                case Category.Warn:
                    this.Logger.LogDebug(message);
                    break;
                case Category.Exception:
                    this.Logger.LogDebug(message);
                    break;
                case Category.Info:
                    this.Logger.LogDebug(message);
                    break;
                default:
                    this.Logger.LogDebug(message);
                    break;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        ///     Configures the container.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            try
            {
                base.ConfigureContainerBuilder(builder);

                builder.RegisterModule<CPIOngConfigContainerModule>();
                builder.RegisterModule<HelperContainerModule>();
                builder.RegisterModule<ConfigLogicLayerContainerModule>();
                builder.RegisterModule<HalPeakCanContainerModule>();
                builder.RegisterType<MainWindow>().As<IMainWindow>().SingleInstance();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        ///     The shell of the application.
        /// </returns>
        /// <remarks>
        ///     If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        ///     <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default
        ///     <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        ///     the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" />
        ///     attached property
        ///     in order to be able to add regions by using the
        ///     <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        ///     attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            var ooo = this.Container.Resolve<IMainWindow>();
            this.Logger = this.Container.Resolve<ILogger>();
            return (MainWindow)ooo;
        }

        /// <summary>
        ///     Initializes the shell.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        protected override void InitializeShell()
        {
            try
            {
                var curDis = Dispatcher.CurrentDispatcher;
                curDis.Thread.GetApartmentState();

                base.InitializeShell();

                Application.Current.MainWindow = (MainWindow)this.Shell;

                if (Application.Current.MainWindow != null)
                {
                    Application.Current.MainWindow.MinWidth = 0;
                    Application.Current.MainWindow.MinHeight = 0;
                    Application.Current.MainWindow.Show();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        #endregion
    }
}