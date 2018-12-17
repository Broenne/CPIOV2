namespace CPIOngConfig.InputBinary
{
    using System;

    using CPIOngConfig.Contracts.InputBinary;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The input binary event handler.
    /// </summary>
    /// <seealso cref="IInputBinaryEventHandler" />
    public class InputBinaryEventHandler : IInputBinaryEventHandler
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="InputBinaryEventHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public InputBinaryEventHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<InputBinaryEventArgs> EventIsReached;

        #endregion

        #region Properties

        private ILogger Logger { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="InputBinaryEventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(InputBinaryEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    throw new Exception("Event for binary input is null");
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