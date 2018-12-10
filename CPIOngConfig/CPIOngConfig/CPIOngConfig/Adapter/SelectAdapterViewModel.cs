using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig.Adapter
{
    using CPIOngConfig.Contracts.Adapter;

    using Prism.Mvvm;
    public class SelectAdapterViewModel : BindableBase, ISelectAdapterViewModel
    {

        private CanAdapter canAdapter;

        public CanAdapter CAnAdapter
        {
            get
            {
                return canAdapter;
            }
            set
            {
                this.SetProperty(ref this.canAdapter, value);
        }
        }

    }
}
