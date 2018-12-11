namespace CPIOngConfig.Contracts.Pulse
{
    using System.Collections.Generic;

    using Prism.Mvvm;

    public class TimePulse : BindableBase
    {
        public TimePulse(string tim)
        {
            this.Tim = tim;
        }

        private string tim;

        public string Tim
        {
            get => tim;
            set => this.SetProperty(ref this.tim, value);
        }
    }



    /// <summary>
    /// The pulse for data view.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class PulseDataForView : BindableBase
    {
        public PulseDataForView(string name)
        {
            this.Name = name;
            this.Times = new List<TimePulse>()
                             {
                                 new TimePulse("12")
                             };

            this.AddTime(3);


        }

        public string Name { get; }

        private List<TimePulse> times;



        public List<TimePulse> Times
        {
            get => times;
                set => this.SetProperty(ref this.times, value);
        }


        public void AddTime(uint dif)
        {
            this.Times.Add(new TimePulse("3"));

            this.Times.Add(new TimePulse("4"));
        }

    }
}
