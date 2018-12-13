using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace CPIOngConfig.ConfigInputs
{
    public class ConfigInputsAllViewModel : BindableBase, IConfigInputsAllViewModel
    {
        public ConfigInputsAllViewModel(IConfigureInputsView configureInputsView)
        {
            //ConfigureInputsViewList = new List<IConfigureInputsView>();
            //ConfigureInputsViewList.Add(configureInputsView);
            this.Test = configureInputsView;
        }


        public IConfigureInputsView Test { get; }

        private List<IConfigureInputsView> configureInputsViewList;

        public List<IConfigureInputsView> ConfigureInputsViewList { get; }
    }
}
