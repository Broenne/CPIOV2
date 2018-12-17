namespace CPIOngConfig.ConfigInputs
{
    using CPIOngConfig.Contracts.ConfigInputs;

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

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the channel.
        /// </summary>
        /// <param name="channelArg">The channel argument.</param>
        public void SetChannel(uint channelArg)
        {
            this.Channel = channelArg;
        }

        #endregion
    }
}