namespace ConfigLogicLayer.DigitalInputState
{
    using System;
    using System.Collections.Generic;

    using ConfigLogicLayer.Contracts.DigitalInputState;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    /// The service for activate debug.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.DigitalInputState.IActivateDebugMode" />
    public class ActivateDebugMode : IActivateDebugMode
    {
        #region Constants

        private const byte Byte0 = 0x02;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateDebugMode"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public ActivateDebugMode(ILogger logger, IWriteBasicCan writeBasicCan)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
        }

        #endregion

        #region Properties

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Resets all.
        /// </summary>
        public void Activate()
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                var data = new List<byte>();
                data.Add(Byte0);
                data.Add(0x01);

                this.WriteBasicCan.WriteCan(0, data);

                // todo mb: wait for alive
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

        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        public void Deactivate()
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                var data = new List<byte>();
                data.Add(Byte0);
                data.Add(0x00);

                this.WriteBasicCan.WriteCan(0, data);

                // todo mb: wait for alive
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

        #endregion
    }
}