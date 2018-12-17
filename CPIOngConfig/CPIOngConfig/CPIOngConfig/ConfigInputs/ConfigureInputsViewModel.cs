namespace CPIOngConfig.ConfigInputs
{
    using System.Collections.Generic;

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

        /// <summary>
        ///     The modus
        /// </summary>
        private List<string> modus;

        #region Constructor

        public ConfigureInputsViewModel()
        {
            this.Modus = new List<string> { "None", "Namur", "Read", "Licht" };
        }

        #endregion

        #region Properties

        public void SetChannel(uint channelArg)
        {
            this.Channel = channelArg;
        }

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
        ///     Gets the modus.
        /// </summary>
        /// <value>
        ///     The modus.
        /// </value>
        public List<string> Modus { get; }

        #endregion
    }
}