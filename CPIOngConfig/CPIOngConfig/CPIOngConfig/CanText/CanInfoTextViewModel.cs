namespace CPIOngConfig.CanText
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Threading;

    using ConfigLogicLayer.Contracts.Text;

    using CPIOngConfig.Contracts.CanText;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    public class CanInfoTextViewModel : BindableBase, ICanInfoTextViewModel
    {
        private readonly Dispatcher dispatcher = RootDispatcherFetcher.RootDispatcher;

        private ObservableCollection<string> text;

        #region Constructor

        public CanInfoTextViewModel(ILogger logger, ICanTextEventHandler canTextEventHandler, IText textService, ITextForDisplayEventHandler textForDisplayEventHandler)
        {
            this.Logger = logger;
            this.CanTextEventHandler = canTextEventHandler;
            this.TextService = textService;
            this.TextForDisplayEventHandler = textForDisplayEventHandler;

            this.Text = new ObservableCollection<string>(new List<string>());

            this.CanTextEventHandler.EventIsReached += this.CanTextEventHandler_EventIsReached;
            this.TextForDisplayEventHandler.EventIsReached += this.TextForDisplayEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        public ObservableCollection<string> Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }

        private ICanTextEventHandler CanTextEventHandler { get; }

        private ILogger Logger { get; }

        private ITextForDisplayEventHandler TextForDisplayEventHandler { get; }

        private IText TextService { get; }

        #endregion

        #region Private Methods

        private void TextForDisplayEventHandler_EventIsReached(object sender, string e)
        {
            try
            {
                this.dispatcher.Invoke(
                    () =>
                        {
                            const int Feldlaenge = 20;
                            if (this.Text.Count > Feldlaenge)
                            {
                                this.Text.RemoveAt(0);
                            }

                            this.Text.Add(e);
                        });
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void CanTextEventHandler_EventIsReached(object sender, byte e)
        {
            try
            {
                if (e != 0)
                {
                    // welche bist sind gesetzt? im byte
                    for (byte i = 0; i < 8; ++i)
                    {
                        if ((e & (1 << i)) != 0)
                        {
                            this.TextService.Request(i);
                        }
                    }
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