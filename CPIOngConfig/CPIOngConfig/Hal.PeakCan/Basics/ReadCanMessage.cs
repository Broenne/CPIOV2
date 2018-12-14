namespace Hal.PeakCan.Basics
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Hal.PeakCan.Contracts.Basics;
    using Hal.PeakCan.PCANDll;

    using HardwareAbstaction.PCAN.Init;
    using HardwareAbstaction.PCAN.PCANDll;

    using HardwareAbstraction.Contracts.PCanDll;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     Read the CAN message.
    /// </summary>
    /// <seealso cref="IReadCanMessage" />
    public class ReadCanMessage : IReadCanMessage
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCanMessage" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="preparePeakCan">The prepare peak can.</param>
        /// <param name="readCanMessageEvent">The read can message event.</param>
        public ReadCanMessage(ILogger logger, IPreparePeakCan preparePeakCan, IReadCanMessageEvent readCanMessageEvent)
        {
            this.ReadCanMessageEvent = readCanMessageEvent;
            try
            {
                logger.LogEnd(this.GetType());

                this.PreparePeakCan = preparePeakCan;

                this.Logger = logger;
                this.MPcanHandle = this.PreparePeakCan.Do();
            }
            catch (Exception e)
            {
                this.Logger.LogError(e);
                throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }

        #endregion

        #region Properties
        
        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>
        ///     The logger.
        /// </value>
        private ILogger Logger { get; }

        /// <summary>
        ///     Gets the MPCAN handle.
        /// </summary>
        /// <value>
        ///     The MPCAN handle.
        /// </value>
        private ushort MPcanHandle { get; }

        private IPreparePeakCan PreparePeakCan { get; }

        private IReadCanMessageEvent ReadCanMessageEvent { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns>
        /// The message event handler.
        /// </returns>
        public IReadCanMessageEvent Start()
        {
            try
            {
                Task.Run(() => { this.ReadRaw(); });
                return this.ReadCanMessageEvent;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion

        #region Private Methods

        private void ReadRaw()
        {
            try
            {
                TPCANStatus result;

                do
                {
                    TpcanMsg readCanMsg;
                    result = PcanBasicDllWrapper.Read(this.MPcanHandle, out readCanMsg, out _);
                    if (result == TPCANStatus.PCAN_ERROR_OK)
                    {
                        this.ReadCanMessageEvent.OnReached(new ReadCanMessageEventArgs(readCanMsg.Id, readCanMsg.Data));
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                while (true);
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