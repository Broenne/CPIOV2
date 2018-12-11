using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig
{
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.InputBinary;
    using CPIOngConfig.Pulse;

    using global::CPIOngConfig.Contracts.ConfigId;

    using Prism.Mvvm;
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IConfigCanId configCanId, ISelectAdapterView selectAdapterView, IInputBinaryView inputBinaryView, IPulseView pulseView)
        {
            this.ConfigCanId = configCanId;
            this.SelectAdapterView = selectAdapterView;
            this.InputBinaryView = inputBinaryView;
            this.PulseView = pulseView;
        }

        public IConfigCanId ConfigCanId { get; }

        public ISelectAdapterView SelectAdapterView { get; }

        public IInputBinaryView InputBinaryView { get; }

        public IPulseView PulseView { get; }


    }
}
