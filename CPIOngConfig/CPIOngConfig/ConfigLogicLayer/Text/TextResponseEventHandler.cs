namespace ConfigLogicLayer.Text
{
    using System;
    using System.Collections.Generic;

    using ConfigLogicLayer.Configurations;

    using CPIOngConfig.Contracts.Alive;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The channel configuration response event handler.
    /// </summary>
    /// <seealso cref="ITextResponseEventHandler" />
    public class TextResponseEventHandler : ITextResponseEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextResponseEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public TextResponseEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<IReadOnlyList<byte>> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="AliveEventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(IReadOnlyList<byte> e)
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