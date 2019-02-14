namespace CPIOngConfig.Contracts.Pulse
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media;

    using Prism.Mvvm;

    /// <summary>
    ///     The pulse for data view.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class PulseDataForView : BindableBase
    {
        private readonly SolidColorBrush gray = new SolidColorBrush(Colors.Gray);

        private readonly uint listCnt;

        private readonly SolidColorBrush white = new SolidColorBrush(Colors.White);

        private bool activated;

        private byte checkSum;

        private SolidColorBrush color;

        private double meanValueFlow;

        private double meanValueTim;

        private TimePulse selectedTimeItem;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PulseDataForView" /> class.
        /// </summary>
        /// <param name="name">The name info.</param>
        /// <param name="listCntArg">The list count argument.</param>
        /// <param name="activatedArg">If set to <c>true</c> [activated argument].</param>
        public PulseDataForView(string name, uint listCntArg, bool activatedArg)
        {
            this.Name = name;
            this.listCnt = listCntArg;

            var helper = new List<TimePulse>();

            for (var i = 0; i < listCntArg; i++)
            {
                helper.Add(new TimePulse(0, 0));
            }

            this.Activated = activatedArg;
            this.Times = new ObservableCollection<TimePulse>(helper);

            this.MeanValueTim = 42;

            this.MeanValueFlow = 56;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="PulseDataForView" /> is activated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if activated; otherwise, <c>false</c>.
        /// </value>
        public bool Activated
        {
            get => this.activated;
            set
            {
                this.Color = value ? this.white : this.gray;
                this.SetProperty(ref this.activated, value);
            }
        }

        /// <summary>
        ///     Gets or sets the check sum.
        /// </summary>
        /// <value>
        ///     The check sum.
        /// </value>
        public byte CheckSum
        {
            get => this.checkSum;

            set => this.SetProperty(ref this.checkSum, value);
        }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public SolidColorBrush Color
        {
            get => this.color;

            set => this.SetProperty(ref this.color, value);
        }

        /// <summary>
        ///     Gets or sets the check sum.
        /// </summary>
        /// <value>
        ///     The check sum.
        /// </value>
        public double MeanValueFlow
        {
            get => this.meanValueFlow;

            set => this.SetProperty(ref this.meanValueFlow, value);
        }

        /// <summary>
        ///     Gets or sets the check sum.
        /// </summary>
        /// <value>
        ///     The check sum.
        /// </value>
        public double MeanValueTim
        {
            get => this.meanValueTim;

            set => this.SetProperty(ref this.meanValueTim, value);
        }

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
        /// <param name="volume">The volume.</param>
        /// <param name="checkSumArg">The check sum.</param>
        public void AddTime(uint dif, double volume, byte checkSumArg)
        {
            this.CheckSum = checkSumArg;
            var timePulseToAdd = new TimePulse(dif, volume);

            this.Times.Add(timePulseToAdd); // das wird gesetz, um den sdcroll balken nach hinten zu setzen
            this.SelectedTimeItem = timePulseToAdd;
            if (this.Times.Count > this.listCnt)
            {
                this.Times.RemoveAt(0);
            }

            var lisTim = this.Times.Select(x => x.Tim).ToList();
            this.MeanValueTim = lisTim.Average();

            var lisFlow = this.Times.Select(x => x.Volume).ToList();
            this.MeanValueFlow = lisFlow.Average();
        }

        #endregion
    }
}