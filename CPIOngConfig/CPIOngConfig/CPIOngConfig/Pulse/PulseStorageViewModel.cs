namespace CPIOngConfig.Pulse
{
    using System;
    using System.Collections.Generic;
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
        private IList<string> storageList;

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
            this.StartCommand = new RelayCommand(this.StartCommandAction);
            this.StopCommand = new RelayCommand(this.StopCommandAction);
            this.SelectedFile = @"C:\Users\tbe241.OSMA-AUFZUEGE\Desktop\savepulse.txt";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the selected file.
        /// </summary>
        /// <value>
        ///     The selected file.
        /// </value>
        public string SelectedFile
        {
            get => this.selectedFile;
            set => this.SetProperty(ref this.selectedFile, value);
        }

        /// <summary>
        ///     Gets or sets the select file command.
        /// </summary>
        /// <value>
        ///     The select file command.
        /// </value>
        public ICommand SelectFileCommand { get; set; }

        /// <summary>
        ///     Gets or sets the start command.
        /// </summary>
        /// <value>
        ///     The start command.
        /// </value>
        public ICommand StartCommand { get; set; }

        /// <summary>
        ///     Gets or sets the stop command.
        /// </summary>
        /// <value>
        ///     The stop command.
        /// </value>
        public ICommand StopCommand { get; set; }

        private ILogger Logger { get; }

        private IPulseEventHandler PulseEventHandler { get; }

        #endregion

        #region Private Methods

        private void StartCommandAction(object obj)
        {
            try
            {
                if (File.Exists(this.SelectedFile))
                {
                    if (this.PulseEventHandler != null)
                    {
                        this.PulseEventHandler.EventIsReached += this.PulseEventHandler_EventIsReached;
                    }

                    this.storageList = new List<string>();
                    for (var i = 0; i < 16; i++)
                    {
                        this.storageList.Add(string.Empty);
                    }
                }
                else
                {
                    MessageBox.Show("Bitte Datei wählen");
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void PulseEventHandler_EventIsReached(object sender, PulseEventArgs e)
        {
            try
            {
                this.storageList[e.Channel] += e.Stamp + ";";
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void StopCommandAction(object obj)
        {
            try
            {
                if (this.PulseEventHandler != null)
                {
                    this.PulseEventHandler.EventIsReached -= this.PulseEventHandler_EventIsReached;

                    // todo mb: das könnte man auch zwichendurch machen
                    var file = new StreamWriter(this.SelectedFile);
                    foreach (var line in this.storageList)
                    {
                        file.WriteLine(line);
                    }

                    file.Close();
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

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