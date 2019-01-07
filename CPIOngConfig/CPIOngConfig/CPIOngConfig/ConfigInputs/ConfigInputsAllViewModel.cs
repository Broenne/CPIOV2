namespace CPIOngConfig.ConfigInputs
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;

    using Autofac;

    using ConfigLogicLayer.Contracts.Configurations;

    using CPIOngConfig.Contracts.ConfigInputs;

    using Helper;
    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The configure inputs all view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="IConfigInputsAllViewModel" />
    public class ConfigInputsAllViewModel : BindableBase, IConfigInputsAllViewModel
    {
        private List<IConfigureInputsView> configureInputsViewList;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigInputsAllViewModel" /> class.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="channelConfiguration">The channel configuration.</param>
        public ConfigInputsAllViewModel(ILifetimeScope scope, ILogger logger, IChannelConfiguration channelConfiguration)
        {
            this.Logger = logger;
            this.ChannelConfiguration = channelConfiguration;
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

        /// <summary>
        /// Gets or sets the configure inputs view list.
        /// </summary>
        /// <value>
        /// The configure inputs view list.
        /// </value>
        public List<IConfigureInputsView> ConfigureInputsViewList
        {
            get => this.configureInputsViewList;

            set => this.SetProperty(ref this.configureInputsViewList, value);
        }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Gets the window load command.
        /// </summary>
        /// <value>
        /// The window load command.
        /// </value>
        public ICommand WindowLoadCommand { get; }

        private IChannelConfiguration ChannelConfiguration { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void WindowLoadCommandAction(object obj)
        {
        }

        private void SaveCommandAction(object obj)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                var listOfChannelConfigurationDto = new List<ChannelConfigurationDto>();
                foreach (var item in this.ConfigureInputsViewList)
                {
                    var ctx = item.GetDataContext();
                    listOfChannelConfigurationDto.Add(new ChannelConfigurationDto(ctx.GetChannel(), ctx.GetSelectedModi()));
                }

                this.ChannelConfiguration.Set(listOfChannelConfigurationDto);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);

                // throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        #endregion
    }
}