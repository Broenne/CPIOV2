namespace CPIOngConfig.Contracts.Pulse
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;

    using Prism.Mvvm;

    /// <summary>
    ///     The pulse for data view.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class PulseDataForView : BindableBase
    {
        private readonly uint listCnt;

        private TimePulse selectedTimeItem;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PulseDataForView" /> class.
        /// </summary>
        /// <param name="name">The name info.</param>
        /// <param name="listCntArg">The list count argument.</param>
        public PulseDataForView(string name, uint listCntArg)
        {
            this.Name = name;
            this.listCnt = listCntArg;

            var helper = new List<TimePulse>();

            for (var i = 0; i < listCntArg; i++)
            {
                helper.Add(new TimePulse("0"));
            }

            this.Times = new ObservableCollection<TimePulse>(helper);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name info.
        /// </value>
        public string Name { get; }

        /// <summary>
        ///     Gets or sets the selected time item.
        /// </summary>
        /// <value>
        ///     The selected time item.
        /// </value>
        public TimePulse SelectedTimeItem
        {
            get => this.selectedTimeItem;
            set => this.SetProperty(ref this.selectedTimeItem, value);
        }

        /// <summary>
        ///     Gets the times.
        /// </summary>
        /// <value>
        ///     The times.
        /// </value>
        public ObservableCollection<TimePulse> Times { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Adds the time.
        /// </summary>
        /// <param name="dif">The difference.</param>
        public void AddTime(double dif)
        {
            var difAsString  = dif.ToString(CultureInfo.InvariantCulture);
            var timePulseToAdd = new TimePulse(difAsString);

            this.Times.Add(timePulseToAdd); // das wird gesetz, um den sdcroll balken nach hinten zu setzen
            this.SelectedTimeItem = timePulseToAdd;
            if (this.Times.Count > this.listCnt)
            {
                this.Times.RemoveAt(0);
            }
        }

        #endregion
    }
}