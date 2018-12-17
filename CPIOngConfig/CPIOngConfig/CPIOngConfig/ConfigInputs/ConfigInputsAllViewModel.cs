namespace CPIOngConfig.ConfigInputs
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Windows.Navigation;

    using Autofac;

    using CPIOngConfig.Contracts.ConfigInputs;

    using Global.UiHelper;

    using Helper.Contracts.Logger;

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
        public ConfigInputsAllViewModel(ILifetimeScope scope, ILogger logger)
        {
            this.Logger = logger;
            this.ConfigureInputsViewList = new List<IConfigureInputsView>();

            for (uint i = 0; i < 16; i++)
            {
                var configureInputsView = scope.Resolve<IConfigureInputsView>();
                var vm = configureInputsView.GetDataContext();
                vm.SetChannel(i);
                this.ConfigureInputsViewList.Add(configureInputsView);
            }

            this.WindowLoadCommand = new RelayCommand(this.WindowLoadCommandAction);
            this.SaveCommand = new RelayCommand(this.SaveCommandAction);
        }

        #endregion

        #region Properties

        private ILogger Logger { get; }

        private void WindowLoadCommandAction(object obj)
        {

        }

        private void SaveCommandAction(object obj)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());


            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }


        public ICommand SaveCommand { get; }

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