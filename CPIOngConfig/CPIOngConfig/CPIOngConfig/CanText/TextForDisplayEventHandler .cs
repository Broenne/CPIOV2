namespace CPIOngConfig.CanText
{
    using System;

    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.CanText;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The input binary event handler.
    /// </summary>
    /// <seealso cref="ITextForDisplayEventHandler" />
    public class TextForDisplayEventHandler : ITextForDisplayEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="CanTextEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public TextForDisplayEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<string> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="string" /> instance containing the event data.</param>
        public virtual void OnReached(string e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    return;
                }

                this.EventIsReached.Invoke(this, e);
            }
            catch (Exception exception)
            {
                this.Logger.LogError(exception);
                throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        #endregion
    }
}