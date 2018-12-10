using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig
{
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.InputBinary;

    using global::CPIOngConfig.Contracts.ConfigId;

    using Prism.Mvvm;
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IConfigCanId configCanId, ISelectAdapterView selectAdapterView, IInputBinaryView inputBinaryView)
        {
            this.ConfigCanId = configCanId;
            this.SelectAdapterView = selectAdapterView;
            this.InputBinaryView = inputBinaryView;
        }

        public IConfigCanId ConfigCanId { get; }

        public ISelectAdapterView SelectAdapterView { get; }

        public IInputBinaryView InputBinaryView { get; }


    }
}
