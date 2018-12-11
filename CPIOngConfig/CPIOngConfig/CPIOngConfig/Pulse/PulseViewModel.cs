using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig.Pulse
{
    using CPIOngConfig.Contracts.Pulse;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="IPulseViewModel" />
    public class PulseViewModel : BindableBase, IPulseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PulseViewModel"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PulseViewModel(ILogger logger)
        {
            this.Logger = logger;
            this.PulseDataForViewList = new List<PulseDataForView>();

            for (int i = 0; i < 16; i++)
            {
                this.PulseDataForViewList.Add(new PulseDataForView($"{i}"));
            }

        }

        private ILogger Logger { get; }


        public List<PulseDataForView> pulseDataForViewList;

        public List<PulseDataForView> PulseDataForViewList
        {
            get => this.pulseDataForViewList;
            set => this.SetProperty(ref this.pulseDataForViewList, value);
        }


    }
}
