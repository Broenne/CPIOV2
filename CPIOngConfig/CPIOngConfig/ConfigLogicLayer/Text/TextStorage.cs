namespace ConfigLogicLayer.Text
{
    using System.Collections.Generic;
    using System.Linq;

        public class TextStorage
        {
            #region Constructor

            public TextStorage(string text)
            {
                this.Data = new Dictionary<byte, string>();
                this.Data.Add(0, text);
            }

            #endregion

            #region Properties

            private Dictionary<byte, string> Data { get; }

            #endregion

            #region Public Methods

            public void AddText(byte posInRow, string txt)
            {
                if (this.Data.ContainsKey(posInRow))
                {
                    return;
                }

                this.Data.Add(posInRow, txt);
            }

        /// <summary>
        /// Creates the finished text.
        /// </summary>
        /// <returns></returns>
        public string CreateFinishedText()
            {
                var tt = this.Data.OrderBy(x => x.Key);

                var result = string.Empty;
                foreach (var item in tt)
                {
                    result += item.Value;
                }

                return result;
            }

            #endregion
        }
    
}