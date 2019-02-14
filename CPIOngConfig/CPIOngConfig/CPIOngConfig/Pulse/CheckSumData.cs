namespace CPIOngConfig.Pulse
{
    public class CheckSumData
    {
        #region Constructor

        public CheckSumData()
        {
            this.IsInitialized = false;
        }

        public CheckSumData(byte checkSum)
        {
            this.IsInitialized = true;
            this.CheckSum = checkSum;
        }

        #endregion

        #region Properties

        private byte CheckSum { get; }

        private bool IsInitialized { get; }

        #endregion

        #region Public Methods

        public bool Check(byte ch)
        {
            if (!this.IsInitialized)
            {
                return true; // kein sinnvoller vergleich wenn kein startwert
            }

            if ((this.CheckSum + 1).Equals(ch))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}