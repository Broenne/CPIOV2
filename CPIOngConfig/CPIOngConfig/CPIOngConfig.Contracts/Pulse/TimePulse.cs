namespace CPIOngConfig.Contracts.Pulse
{
    using Prism.Mvvm;

    public class TimePulse : BindableBase
    {
        private string tim;

        #region Constructor

        public TimePulse(string tim)
        {
            this.Tim = tim;
        }

        #endregion

        #region Properties

        public string Tim
        {
            get => this.tim;
            set => this.SetProperty(ref this.tim, value);
        }

        #endregion
    }
}