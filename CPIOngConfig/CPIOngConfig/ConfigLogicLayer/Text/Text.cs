using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigLogicLayer.Text
{
    using System.Linq;
    using System.Windows;

    using ConfigLogicLayer.Configurations;
    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Text;

    using CPIOngConfig.CanText;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    /// The text service.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.Text.IText" />
    public class Text : IText
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="writeBasicCan">The write basic can.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="textResponseEventHandler">The text response event handler.</param>
        public Text(IWriteBasicCan writeBasicCan, ILogger logger, ITextResponseEventHandler textResponseEventHandler, IGetActualNodeId getActualNodeId, ITextForDisplayEventHandler textForDisplayEventHandler)
        {
            this.WriteBasicCan = writeBasicCan;
            this.Logger = logger;
            this.TextResponseEventHandler = textResponseEventHandler;
            this.GetActualNodeId = getActualNodeId;
            this.TextForDisplayEventHandler = textForDisplayEventHandler;

            this.TextFromCpio = new List<TextStorage>(new TextStorage[8]);

            this.TextResponseEventHandler.EventIsReached += TextResponseEventHandler_EventIsReached;
        }

        private ITextForDisplayEventHandler TextForDisplayEventHandler { get; }

        private void TextResponseEventHandler_EventIsReached(object sender, IReadOnlyList<byte> e)
        {
            try
            {
                bool endIsReached = false;
                string txt = string.Empty;

                // nur byte 3 bis \n sind nutzdaten
                for (int i = 3; i < 8; i++)
                {
                    char content = Convert.ToChar(e[i]);
                    //if(/*e[i] == 10 ||*/ e[i] == 0)
                    if (e[i] == 10)
                    //if(e[i] == 0x2F) // das backslash
                    {
                        endIsReached = true;
                        break;
                    }

                    txt += content;
                }
                
                var postion = e[0];
                byte positionInRow = e[1];
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
                    byte nextpositionInRow = (byte)(positionInRow + 5);

                    if (nextpositionInRow > 200)
                    {
                        MessageBox.Show("Notasugang, wenn zeile zu lang");
                        return;
                    }

                    this.Request(postion, nextpositionInRow);
                }

                if (endIsReached)
                {
                    var newFinishedText = this.TextFromCpio[postion].CreateFinishedText();
                    Console.WriteLine($"newFinishedText {newFinishedText}");
                    this.TextForDisplayEventHandler.OnReached(newFinishedText);
                }

            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }



        private List<TextStorage> TextFromCpio { get; }

        private class TextStorage
        {
            public TextStorage(string text)
            {
                Data = new Dictionary<byte, string>();
                Data.Add(0, text);
            }

            private Dictionary<byte, string> Data { get; }

            public void AddText(byte posInRow, string txt)
            {
                if (this.Data.ContainsKey(posInRow))
                {
                    return;
                }

                this.Data.Add(posInRow, txt);
            }

            public string CreateFinishedText()
            {
                var tt = this.Data.OrderBy(x => x.Key);

                string result = string.Empty;
                foreach (var item in tt)
                {
                    result += item.Value;
                }

                return result;
            }
        }



        private IWriteBasicCan WriteBasicCan { get; }

        private ILogger Logger { get; }

        private ITextResponseEventHandler TextResponseEventHandler { get; }

        private IGetActualNodeId GetActualNodeId { get; }



        /// <summary>
        /// Requests the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        public void Request(byte position, byte positionInRow = 0)
        {
            try
            {
                var data = new List<byte>();
                data.Add(position);
                data.Add(positionInRow);
                this.WriteBasicCan.WriteCan(this.GetActualNodeId.Get() + CanCommandConsts.Text, data);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }
    }
}
