namespace ConfigLogicLayer.Text
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The text storage service.
    /// </summary>
    public class TextStorage
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextStorage" /> class.
        /// </summary>
        /// <param name="text">The text info.</param>
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

        /// <summary>
        /// Adds the text.
        /// </summary>
        /// <param name="posInRow">The position in row.</param>
        /// <param name="txt">The text info.</param>
        public void AddText(byte posInRow, string txt)
        {
            if (this.Data.ContainsKey(posInRow))
            {
                return;
            }

            this.Data.Add(posInRow, txt);
        }

        /// <summary>
        ///     Creates the finished text.
        /// </summary>
        /// <returns>Return the text.</returns>
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