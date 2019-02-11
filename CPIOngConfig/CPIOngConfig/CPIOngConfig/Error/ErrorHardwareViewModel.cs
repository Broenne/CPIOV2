namespace CPIOngConfig.Error
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Media;

    using ConfigLogicLayer.Contracts;

    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.Error;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The error hardware view model.
    /// </summary>
    /// <seealso cref="CPIOngConfig.Contracts.Error.IErrorHardwareViewModel" />
    public class ErrorHardwareViewModel : BindableBase, IErrorHardwareViewModel
    {
        private readonly SolidColorBrush green = new SolidColorBrush(Colors.Green);

        private readonly SolidColorBrush red = new SolidColorBrush(Colors.Red);

        private ObservableCollection<SolidColorBrush> color;

        private string rawErrorFildData;

        #region Constructor

        public ErrorHardwareViewModel(ILogger logger, IAliveEventHandler aliveEventHandler)
        {
            this.Logger = logger;
            this.AliveEventHandler = aliveEventHandler;

            this.Color = new ObservableCollection<SolidColorBrush>();

            for (var i = 0; i < CanCommandConsts.CountOfErrorBytes * 8; i++)
            {
                this.Color.Add(this.red);
            }

            this.AliveEventHandler.EventIsReached += this.AliveEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public ObservableCollection<SolidColorBrush> Color
        {
            get => this.color;

            set => this.SetProperty(ref this.color, value);
        }

        /// <summary>
        ///     Gets or sets the raw error fild data.
        /// </summary>
        /// <value>
        ///     The raw error fild data.
        /// </value>
        public string RawErrorFildData
        {
            get => this.rawErrorFildData;
            set => this.SetProperty(ref this.rawErrorFildData, value);
        }

        private IAliveEventHandler AliveEventHandler { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void AliveEventHandler_EventIsReached(object sender, AliveEventArgs e)
        {
            try
            {
                this.Color[0] = this.green;

                for (var i = 0; i < 32; ++i)
                {
                    var hasError = e.Errors[i / 8] << (i % 8) == 1;

                    this.Color[i] = hasError ? this.green : this.red;
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}