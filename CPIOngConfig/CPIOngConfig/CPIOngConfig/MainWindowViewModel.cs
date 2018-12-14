namespace CPIOngConfig
{
    using CPIOngConfig.ConfigInputs;
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.Contracts.Analog;
    using CPIOngConfig.Contracts.ConfigId;
    using CPIOngConfig.InputBinary;
    using CPIOngConfig.Pulse;

    using Prism.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        #region Constructor

        public MainWindowViewModel(IConfigCanId configCanId, ISelectAdapterView selectAdapterView, IInputBinaryView inputBinaryView, IConfigInputsAllView configInputsAllView, IAnalogView analogView, IPulseView pulseView)
        {
            this.ConfigCanId = configCanId;
            this.SelectAdapterView = selectAdapterView;
            this.InputBinaryView = inputBinaryView;
            this.ConfigInputsAllView = configInputsAllView;
            this.AnalogView = analogView;
            this.PulseView = pulseView;
        }

        #endregion

        #region Properties

        public IAnalogView AnalogView { get; }

        public IConfigCanId ConfigCanId { get; }

        public IConfigInputsAllView ConfigInputsAllView { get; }

        public IInputBinaryView InputBinaryView { get; }

        public IPulseView PulseView { get; }

        public ISelectAdapterView SelectAdapterView { get; }

        #endregion
    }
}