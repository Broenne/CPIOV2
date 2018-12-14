namespace CPIOngConfig.Contracts.Pulse
{
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

    using Prism.Mvvm;

    /// <summary>
    ///     The pulse for data view.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class PulseDataForView : BindableBase
    {
        private readonly uint listCnt;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseDataForView" /> class.
        /// </summary>
        /// <param name="name">The name info.</param>
        /// <param name="listCntArg">The list count argument.</param>
        public PulseDataForView(string name, uint listCntArg)
        {
            this.Name = name;
            this.listCnt = listCntArg;
            this.Times = new ObservableCollection<TimePulse>();

            // fill with leading 0
            for (var i = 0; i < listCntArg; i++)
            {
                this.Times.Add(new TimePulse("0"));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name info.
        /// </value>
        public string Name { get; }
        
        /// <summary>
        /// Gets the times.
        /// </summary>
        /// <value>
        /// The times.
        /// </value>
        public ObservableCollection<TimePulse> Times { get; }

        private TimePulse selectedTimeItem;
        public TimePulse SelectedTimeItem
        {
            get
            {
                return this.selectedTimeItem;
            }
            set
            {
                this.SetProperty(ref this.selectedTimeItem, value);
            }

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the time.
        /// </summary>
        /// <param name="dif">The difference.</param>
        public void AddTime(uint dif)
        {
            var timePulseToAdd = new TimePulse(dif.ToString());
            this.Times.Add(timePulseToAdd);
            this.SelectedTimeItem = timePulseToAdd;
            if (this.Times.Count > this.listCnt)
            {
                this.Times.RemoveAt(0);
            }
        }

        #endregion
    }
}