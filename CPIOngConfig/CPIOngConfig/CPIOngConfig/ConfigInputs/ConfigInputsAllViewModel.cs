namespace CPIOngConfig.ConfigInputs
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Windows.Navigation;

    using Autofac;

    using CPIOngConfig.Contracts.ConfigInputs;

    using Global.UiHelper;

    using Prism.Mvvm;

    /// <summary>
    /// The configure inputs all view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="IConfigInputsAllViewModel" />
    public class ConfigInputsAllViewModel : BindableBase, IConfigInputsAllViewModel
    {
        private List<IConfigureInputsView> configureInputsViewList;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigInputsAllViewModel" /> class.
        /// </summary>
        /// <param name="scope">The scope.</param>
        public ConfigInputsAllViewModel(ILifetimeScope scope)
        {
            this.ConfigureInputsViewList = new List<IConfigureInputsView>();

            for (uint i = 0; i < 16; i++)
            {
                var configureInputsView = scope.Resolve<IConfigureInputsView>();
                var vm = configureInputsView.GetDataContext();
                vm.SetChannel(i);
                this.ConfigureInputsViewList.Add(configureInputsView);
            }

            this.WindowLoadCommand = new RelayCommand(this.WindowLoadCommandAction);
        }

        #endregion

        #region Properties

        private void WindowLoadCommandAction(object obj)
        {

        }

        public ICommand WindowLoadCommand { get; }


        public List<IConfigureInputsView> ConfigureInputsViewList
        {
            get
            {
                return this.configureInputsViewList;
            }

            set
            {
                this.SetProperty(ref this.configureInputsViewList, value);
            }
        }

        #endregion
    }
}