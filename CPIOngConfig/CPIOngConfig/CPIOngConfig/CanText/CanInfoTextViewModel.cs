namespace CPIOngConfig.CanText
{
    using System;
    using System.Windows;

    using CPIOngConfig.Contracts.CanText;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class CanInfoTextViewModel : BindableBase, ICanInfoTextViewModel
    {
        private string text;

        #region Constructor

        public CanInfoTextViewModel(ILogger logger, ICanTextEventHandler canTextEventHandler)
        {
            this.Logger = logger;
            this.CanTextEventHandler = canTextEventHandler;
            this.CanTextEventHandler.EventIsReached += this.CanTextEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        public string Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }

        private ICanTextEventHandler CanTextEventHandler { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void CanTextEventHandler_EventIsReached(object sender, byte e)
        {
            try
            {
                if (e != 0)
                {


                    // welche bist sind gesetzt? im byte
                    for (int i = 0; i < 8; ++i)
                    {

                        if ( (e & 1 << i) != 0)
                        {
                            this.Text += $"Bit {i} is set \r\n";
                        }
                    }
                    

                    // info in dto speichern, und abholung starten
                    this.Text += e.ToString();
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