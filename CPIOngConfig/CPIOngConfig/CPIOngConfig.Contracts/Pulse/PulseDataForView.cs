namespace CPIOngConfig.Contracts.Pulse
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Threading;
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

    /// <summary>
    ///     The pulse for data view.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class PulseDataForView : BindableBase
    {
        private List<TimePulse> times;

        #region Constructor

        public PulseDataForView(string name)
        {
            this.Name = name;
            this.Times = new ObservableCollection<TimePulse>();
        }

        #endregion

        #region Properties,



        public string Name { get; }

        //public List<TimePulse> Times
        //{
        //    get => this.times;
        //    set => this.SetProperty(ref this.times, value);
        //}


        public ObservableCollection<TimePulse> Times { get; }
        #endregion

        #region Public Methods

        public void AddTime(uint dif)
        {
            //Internal.Add(new TimePulse(dif.ToString()));
            this.Times.Add( new TimePulse(dif.ToString()));// = this.Internal;

            if (this.Times.Count > 100)
            {
                this.Times.RemoveAt(0);
            }

        }

        #endregion
    }
}