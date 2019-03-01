namespace CPIOngConfig.ConfigInputs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using Autofac;

    using ConfigLogicLayer.Contracts.Configurations;

    using CPIOngConfig.Contracts.Adapter;
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

        private bool isEnabled;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigInputsAllViewModel" /> class.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="channelConfiguration">The channel configuration.</param>
        /// <param name="canIsConnectedEventHandler">The can is connected event handler.</param>
        public ConfigInputsAllViewModel(ILifetimeScope scope, ILogger logger, IChannelConfiguration channelConfiguration, ICanIsConnectedEventHandler canIsConnectedEventHandler)
        {
            this.Logger = logger;
            this.ChannelConfiguration = channelConfiguration;
            canIsConnectedEventHandler.EventIsReached += this.CanIsConnectedEventHandler_EventIsReached;
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
            this.LoadFromDeviceCommand = new RelayCommand(this.LoadFromDeviceCommandAction);
            this.SetAllToFirstOneCommand = new RelayCommand(this.SetAllToFirstOneCommandAction);
            this.SetAllToSpecialCommand = new RelayCommand(this.SetAllToSpecialCommandAction);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the configure inputs view list.
        /// </summary>
        /// <value>
        ///     The configure inputs view list.
        /// </value>
        public List<IConfigureInputsView> ConfigureInputsViewList
        {
            get => this.configureInputsViewList;

            set => this.SetProperty(ref this.configureInputsViewList, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c>If this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get => this.isEnabled;

            set => this.SetProperty(ref this.isEnabled, value);
        }

        /// <summary>
        ///     Gets the load from device command.
        /// </summary>
        /// <value>
        ///     The load from device command.
        /// </value>
        public ICommand LoadFromDeviceCommand { get; }

        /// <summary>
        ///     Gets the save command.
        /// </summary>
        /// <value>
        ///     The save command.
        /// </value>
        public ICommand SaveCommand { get; }

        /// <summary>
        ///     Gets the set all to first one command.
        /// </summary>
        /// <value>
        ///     The set all to first one command.
        /// </value>
        public ICommand SetAllToFirstOneCommand { get; }

        public ICommand SetAllToSpecialCommand { get; set; }

        /// <summary>
        ///     Gets the window load command.
        /// </summary>
        /// <value>
        ///     The window load command.
        /// </value>
        public ICommand WindowLoadCommand { get; }

        private IChannelConfiguration ChannelConfiguration { get; }

        private ILogger Logger { get; }

        #endregion

        #region Private Methods

        private void CanIsConnectedEventHandler_EventIsReached(object sender, bool e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                this.IsEnabled = e;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        private void SetAllToFirstOneCommandAction(object obj)
        {
            try
            {
                var first = this.ConfigureInputsViewList.FirstOrDefault();

                if (first == null)
                {
                    return;
                }

                foreach (var item in this.ConfigureInputsViewList)
                {
                    var vm = item.GetDataContext();
                    vm.ChangeChannelModi(first.GetDataContext().GetSelectedModi());
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        private void SetAllToSpecialCommandAction(object obj)
        {
            try
            {
                this.ConfigureInputsViewList[0].GetDataContext().ChangeChannelModi(Modi.Licht);
                this.ConfigureInputsViewList[1].GetDataContext().ChangeChannelModi(Modi.Licht);
                this.ConfigureInputsViewList[2].GetDataContext().ChangeChannelModi(Modi.Licht);

                this.ConfigureInputsViewList[3].GetDataContext().ChangeChannelModi(Modi.Read);
                this.ConfigureInputsViewList[4].GetDataContext().ChangeChannelModi(Modi.Read);
                this.ConfigureInputsViewList[5].GetDataContext().ChangeChannelModi(Modi.Read);

                this.ConfigureInputsViewList[6].GetDataContext().ChangeChannelModi(Modi.Namur);
                this.ConfigureInputsViewList[7].GetDataContext().ChangeChannelModi(Modi.Namur);
                this.ConfigureInputsViewList[8].GetDataContext().ChangeChannelModi(Modi.Namur);

                this.ConfigureInputsViewList[9].GetDataContext().ChangeChannelModi(Modi.Qmin);
                this.ConfigureInputsViewList[10].GetDataContext().ChangeChannelModi(Modi.Qmin);
                this.ConfigureInputsViewList[11].GetDataContext().ChangeChannelModi(Modi.Qmin);

                this.ConfigureInputsViewList[12].GetDataContext().ChangeChannelModi(Modi.Qmax);
                this.ConfigureInputsViewList[13].GetDataContext().ChangeChannelModi(Modi.Qmax);
                this.ConfigureInputsViewList[14].GetDataContext().ChangeChannelModi(Modi.Qmax);

                this.ConfigureInputsViewList[15].GetDataContext().ChangeChannelModi(Modi.None);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        private void LoadFromDeviceCommandAction(object obj)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                this.ChannelConfiguration.TriggerToGetState();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.Logger.LogEnd(this.GetType());
            }
        }

        private void WindowLoadCommandAction(object obj)
        {
        }

        private void SaveCommandAction(object obj)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                Mouse.OverrideCursor = Cursors.Wait;

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
                Mouse.OverrideCursor = null;
                this.Logger.LogEnd(this.GetType());
            }
        }

        #endregion
    }
}