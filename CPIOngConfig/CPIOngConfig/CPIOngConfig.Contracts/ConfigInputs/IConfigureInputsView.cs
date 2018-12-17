namespace CPIOngConfig.Contracts.ConfigInputs
{
    using CPIOngConfig.ConfigInputs;

    /// <summary>
    ///     The configure inputs view.
    /// </summary>
    public interface IConfigureInputsView
    {
        #region Public Methods

        /// <summary>
        ///     Gets the get data context.
        /// </summary>
        /// <returns>Return the view model.</returns>
        /// <value>
        ///     The get data context.
        /// </value>
        IConfigureInputsViewModel GetDataContext();

        #endregion
    }
}