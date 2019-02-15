namespace CPIOngConfig.ConfigInputs
{
    using System;

    using ConfigLogicLayer.Contracts.Configurations;

    using CPIOngConfig.Contracts.ConfigInputs;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    ///     The configure inputs view model.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="IConfigureInputsViewModel" />
    public class ConfigureInputsViewModel : BindableBase, IConfigureInputsViewModel
    {
        private uint channel;

        private Modi seletedModi;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureInputsViewModel"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="channelConfigurationResponseEventHandler">The channel configuration response event handler.</param>
        public ConfigureInputsViewModel(ILogger logger, IChannelConfigurationResponseEventHandler channelConfigurationResponseEventHandler)
        {
            this.Logger = logger;
            this.ChannelConfigurationResponseEventHandler = channelConfigurationResponseEventHandler;
            this.ChannelConfigurationResponseEventHandler.EventIsReached += this.ChannelConfigurationResponseEventHandler_EventIsReached;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the channel.
        /// </summary>
        /// <value>
        ///     The channel.
        /// </value>
        public uint Channel
        {
            get => this.channel;
            set => this.SetProperty(ref this.channel, value);
        }

        /// <summary>
        ///     Gets or sets the selected modi.
        /// </summary>
        /// <value>
        ///     The selected modi.
        /// </value>
        public Modi SelectedModi
        {
            get => this.seletedModi;
            set => this.SetProperty(ref this.seletedModi, value);
        }

        private IChannelConfigurationResponseEventHandler ChannelConfigurationResponseEventHandler { get; }

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the channel.
        /// </summary>
        /// <returns>
        ///     Return the channel.
        /// </returns>
        public uint GetChannel()
        {
            return this.Channel;
        }

        /// <summary>
        ///     Gets the selected modi.
        /// </summary>
        /// <returns>Return the modus.</returns>
        public Modi GetSelectedModi()
        {
            return this.SelectedModi;
        }

        /// <summary>
        ///     Sets the channel.
        /// </summary>
        /// <param name="channelArg">The channel argument.</param>
        public void SetChannel(uint channelArg)
        {
            this.Channel = channelArg;
        }

        /// <summary>
        /// Changes the channel modi.
        /// </summary>
        /// <param name="modi">The modi to use.</param>
        public void ChangeChannelModi(Modi modi)
        {
            this.SelectedModi = modi;
        }

        #endregion

        #region Private Methods

        private void ChannelConfigurationResponseEventHandler_EventIsReached(object sender, ChannelConfigurationResponseEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                if (e.Channel.Equals(this.channel))
                {
                    this.SelectedModi = e.Modi;
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion
    }
}