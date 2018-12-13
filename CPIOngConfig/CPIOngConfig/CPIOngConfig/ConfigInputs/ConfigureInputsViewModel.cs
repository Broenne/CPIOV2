using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace CPIOngConfig.ConfigInputs
{



    public class ConfigureInputsViewModel : BindableBase, IConfigureInputsViewModel
    {
        public ConfigureInputsViewModel()
        {
            this.Modus = new List<string>()
            {
                "None",
                "Namur",
                "Read",
                "Licht"
            };
        }

        // tdo mb: modus in eins umschalten

        private string channel;
        public string Channel
        {
            get => this.channel;
            set => this.SetProperty(ref channel, value);
        }

        private List<string> modus;
        public List<string> Modus { get; }


    }
}
