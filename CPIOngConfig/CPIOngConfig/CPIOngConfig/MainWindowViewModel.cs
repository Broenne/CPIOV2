using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig
{
    using CPIOngConfig.Analog;
    using CPIOngConfig.ConfigInputs;
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.InputBinary;
    using CPIOngConfig.Pulse;

    using global::CPIOngConfig.Contracts.ConfigId;

    using Prism.Mvvm;
    
        public class MainWindowViewModel : BindableBase
        {
            public MainWindowViewModel(IConfigCanId configCanId, ISelectAdapterView selectAdapterView, IInputBinaryView inputBinaryView, IConfigInputsAllView configInputsAllView, IAnalogView analogView)
            {
                this.ConfigCanId = configCanId;
                this.SelectAdapterView = selectAdapterView;
                this.InputBinaryView = inputBinaryView;
                this.ConfigInputsAllView = configInputsAllView;
                this.AnalogView = analogView;
            }

            public IConfigCanId ConfigCanId { get; }

            public ISelectAdapterView SelectAdapterView { get; }

            public IInputBinaryView InputBinaryView { get; }

            public IConfigInputsAllView ConfigInputsAllView { get; }

            private IAnalogView AnalogView { get; }
        }


    
}
