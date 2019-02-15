namespace CPIOngConfig.Pulse
{
    /// <summary>
    /// Service for check sum data.
    /// </summary>
    public class CheckSumData
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckSumData"/> class.
        /// </summary>
        public CheckSumData()
        {
            this.IsInitialized = false;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="CheckSumData"/> class.
        ///// </summary>
        ///// <param name="checkSum">The check sum.</param>
        //public CheckSumData(byte checkSum)
        //{
        //    this.IsInitialized = true;
        //    this.CheckSum = checkSum;
        //}


        public void ChangeCheckSum(byte checkSum)
        {
            this.IsInitialized = true;
            this.CheckSum = checkSum;
        }


        #endregion

        #region Properties

        private byte CheckSum { get; set; }

        private bool IsInitialized { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks the specified count.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="ch">The check sum..</param>
        /// <param name="info">The information.</param>
        /// <returns>
        /// Return if check is valid.
        /// </returns>
        public bool Check(int channel, byte ch, ref string info)
        {
            if (!this.IsInitialized)
            {
                return true; // kein sinnvoller vergleich wenn kein startwert
            }

            // überlauf return true
            if (this.CheckSum.Equals(255) && ch.Equals(0))
            {
                return true;
            }

            if ((this.CheckSum + 1).Equals(ch))
            {
                return true;
            }

            if (this.CheckSum == ch)
            {
                info = $"Gleiche {channel} checkSum:{ch}.";

                // todo mb: ignore
                return true;
            }
            else
            {
                info = $"Puls-Reihenfolge passt nicht Kanal:{channel} checkSum:{ch}.";
            }

            return false;
        }

        #endregion
    }
}