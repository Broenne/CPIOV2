using System.Threading;
using System.Threading.Tasks;
using Hal.PeakCan.Contracts.Basics;

namespace HardwareAbstaction.PCAN.Basics
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;


    using Hal.PeakCan.PCANDll;

    using HardwareAbstaction.PCAN.Init;
    using HardwareAbstaction.PCAN.PCANDll;

    using HardwareAbstraction.Contracts.PCanDll;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     Read the CAN message.
    /// </summary>
    /// <seealso cref="IReadCanMessage" />
    public class ReadCanMessage : IReadCanMessage //: IReadCanMessage
    {
        private static readonly object LockRead = new object();

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReadCanMessage" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="preparePeakCan">The prepare peak can.</param>
        public ReadCanMessage(ILogger logger, IPreparePeakCan preparePeakCan, IReadCanMessageEvent readCanMessageEvent)
        {
            ReadCanMessageEvent = readCanMessageEvent;
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

        //#region Properties

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


        public void Start()
        {
            try
            {
                Task.Run(() =>
                {
                    this.ReadRaw();

                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }        


        private IReadCanMessageEvent ReadCanMessageEvent { get; }

        //#region Private Methods

        private void ReadRaw()
        {
            try
            {
                TPCANStatus result;

                
                do
                {
                    TpcanMsg readCanMsg;
                    //PcanBasicDllWrapper.FilterMessages(this.MPcanHandle, id, id, TpcanMode.PCAN_MODE_STANDARD);
                    result = PcanBasicDllWrapper.Read(this.MPcanHandle, out readCanMsg, out _);
                    if (result == TPCANStatus.PCAN_ERROR_OK)
                    {
                        this.ReadCanMessageEvent.OnReached(new ReadCanMessageEventArgs(readCanMsg.Id, readCanMsg.Data));
                       // send event
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                while (true);

                ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }     

    }
}