namespace CPIOngConfig.Pulse
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;

    using CPIOngConfig.Contracts.Pulse;

    using Helper;
    using Helper.Contracts.Logger;

    using Microsoft.Win32;

    using Prism.Mvvm;

    /// <summary>
    ///     The view model for pulse storage.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.Contracts.Pulse.IPulseStorageViewModel" />
    public class PulseStorageViewModel : BindableBase, IPulseStorageViewModel
    {
        private string selectedFile;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PulseStorageViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pulseEventHandler">The pulse event handler.</param>
        public PulseStorageViewModel(ILogger logger, IPulseEventHandler pulseEventHandler)
        {
            this.Logger = logger;
            this.PulseEventHandler = pulseEventHandler;
            this.SelectFileCommand = new RelayCommand(this.SelectFileCommandAction);
        }

        #endregion

        #region Properties

        public string SelectedFile
        {
            get => this.selectedFile;
            set => this.SetProperty(ref this.selectedFile, value);
        }

        public ICommand SelectFileCommand { get; set; }

        public ICommand StartCommand { get; set; }

        public ICommand StopCommand { get; set; }

        private ILogger Logger { get; }

        private IPulseEventHandler PulseEventHandler { get; }

        #endregion

        #region Private Methods

        private void SelectFileCommandAction(object obj)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    this.SelectedFile = openFileDialog.FileName;
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