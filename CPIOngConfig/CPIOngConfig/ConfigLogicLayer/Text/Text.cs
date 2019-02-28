namespace ConfigLogicLayer.Text
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using ConfigLogicLayer.Configurations;
    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Text;

    using CPIOngConfig.CanText;

    using Hal.PeakCan.Contracts.Basics;

    using HardwareAbstraction.Contracts.PCanDll;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The text service.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.Text.IText" />
    public class Text : IText
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Text" /> class.
        /// </summary>
        /// <param name="writeBasicCan">The write basic can.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="textResponseEventHandler">The text response event handler.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        /// <param name="textForDisplayEventHandler">The text for display event handler.</param>
        public Text(IWriteBasicCan writeBasicCan, ILogger logger, ITextResponseEventHandler textResponseEventHandler, IGetActualNodeId getActualNodeId, ITextForDisplayEventHandler textForDisplayEventHandler)
        {
            this.WriteBasicCan = writeBasicCan;
            this.Logger = logger;
            this.TextResponseEventHandler = textResponseEventHandler;
            this.GetActualNodeId = getActualNodeId;
            this.TextForDisplayEventHandler = textForDisplayEventHandler;

            this.TextFromCpio = new List<TextStorage>(new TextStorage[8]);

            this.TextResponseEventHandler.EventIsReached += this.TextResponseEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        private IGetActualNodeId GetActualNodeId { get; }

        private ILogger Logger { get; }

        private ITextForDisplayEventHandler TextForDisplayEventHandler { get; }

        private List<TextStorage> TextFromCpio { get; }

        private ITextResponseEventHandler TextResponseEventHandler { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Requests the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="positionInRow">The position in row.</param>
        public void Request(byte position, byte positionInRow = 0)
        {
            try
            {
                var data = new List<byte>();
                data.Add(position);
                data.Add(positionInRow);
                this.WriteBasicCan.WriteCan(this.GetActualNodeId.Get() + CanCommandConsts.Text, data, TpcanMessageType.PCAN_MESSAGE_EXTENDED);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion

        #region Private Methods
        
        private void TextResponseEventHandler_EventIsReached(object sender, IReadOnlyList<byte> e)
        {
            try
            {
                var endIsReached = false;
                var txt = string.Empty;

                // nur byte 3 bis \n sind nutzdaten
                for (var i = 3; i < 8; i++)
                {
                    var content = Convert.ToChar(e[i]);
                    if (e[i] == 10) 
                    {
                        // ascii \n
                        endIsReached = true;
                        break;
                    }

                    txt += content;
                }

                var postion = e[0];
                var positionInRow = e[1];
                if (positionInRow == 0)
                {
                    // wennn position 0 ist, dann hinten anfügen
                    this.TextFromCpio[postion] = new TextStorage(txt);
                }
                else
                {
                    this.TextFromCpio[postion].AddText(positionInRow, txt);
                }

                if (!endIsReached)
                {
                    var nextpositionInRow = (byte)(positionInRow + 5); // weitersetzen der Position im string der cpio

                    if (nextpositionInRow > 200)
                    {
                        MessageBox.Show("Notausgang, wenn zeile zu lang");
                        return;
                    }

                    this.Request(postion, nextpositionInRow);
                }

                if (endIsReached)
                {
                    var newFinishedText = this.TextFromCpio[postion].CreateFinishedText();
                    this.TextForDisplayEventHandler.OnReached(newFinishedText);
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion
    }
}