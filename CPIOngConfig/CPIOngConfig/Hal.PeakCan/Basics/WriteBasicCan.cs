namespace Hal.PeakCan.Basics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Hal.PeakCan.Contracts.Basics;
    using Hal.PeakCan.Contracts.Init;
    using Hal.PeakCan.PCANDll;

    using HardwareAbstaction.PCAN.PCANDll;

    using HardwareAbstraction.Contracts.PCanDll;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The write basic CAN class.
    /// </summary>
    public class WriteBasicCan : IWriteBasicCan
    {
        private static readonly object LockWrite = new object();

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="WriteBasicCan" /> class.
        /// </summary>
        /// <param name="preparePeakCan">The prepare peak can.</param>
        /// <param name="logger">The logger.</param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public WriteBasicCan(IPreparePeakCan preparePeakCan, ILogger logger)
        {
            this.PreparePeakCan = preparePeakCan;
            this.Logger = logger;
        }

        #endregion

        #region Properties

        private ILogger Logger { get; }

        private ushort MPcanHandle { get; set; }

        private IPreparePeakCan PreparePeakCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the actual handle.
        /// </summary>
        /// <returns>Return the actual handle.</returns>
        public string GetActualHandle()
        {
            return this.MPcanHandle.ToString("X"); // todo mb: hier namen rein bauen?!
        }

        /// <summary>
        ///     Remotes the request for channel value.
        /// </summary>
        /// <param name="node">The node value.</param>
        public void RemoteRequestForChannelValue(uint node)
        {
            try
            {
                this.OpenCanDongle();

                // ClearMessageBuffer(node);
                var canMsg = new TpcanMsg
                                 {
                                     Id = node,
                                     Msgtype = TpcanMessageType.PCAN_MESSAGE_RTR,
                                     Data = new byte[8],
                                     Len = 2 // todo mb: die längenangabe ist aufgrund eines Fehlverhaltens im alten Knoten notwendig
                                 };

                // Console.WriteLine("canMsg.ID: RemoteRequest" + canMsg.ID);
                var result = PcanBasicDllWrapper.Write(this.MPcanHandle, ref canMsg);

                if (result != TPCANStatus.PCAN_ERROR_OK)
                {
                    throw new Exception("TpCanStatus");
                }
            }
            catch (Exception e)
            {
                this.Logger.LogError(e);
            }
        }

        ///// <summary>
        /////     Clears the message buffer.
        ///// </summary>
        ///// <param name="type">The type of message.</param>
        // public void ClearMessageBuffer(uint id, TpcanMessageType type = TpcanMessageType.PCAN_MESSAGE_STANDARD)
        // {
        // TpcanMsg readCanMsg;
        // readCanMsg.Id = 0x1B;
        // readCanMsg.Len = 8;

        // readCanMsg.Msgtype = type;

        // while (TPCANStatus.PCAN_ERROR_QRCVEMPTY
        // != PcanBasicDllWrapper.Read(this.MPcanHandle, out readCanMsg, out _))
        // {
        // // Console.WriteLine($"clear {readCanMsg.Id}");
        // this.Logger.LogTrace("read empty" + readCanMsg.Id);
        // }
        // }

        /// <summary>
        ///     Writes the can.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data info.</param>
        /// <exception cref="Exception">TPCANSTATUS not ok.</exception>
        public void WriteCan(uint id, IReadOnlyList<byte> data)
        {
            try
            {
                lock (LockWrite)
                {
                    this.Logger.LogBegin(this.GetType());

                    this.OpenCanDongle();

                    if (data.Count > 8)
                    {
                        throw new Exception("Could not write more then 8 bytes.");
                    }

                    var canMsg = new TpcanMsg();
                    canMsg.Id = id;
                    canMsg.Data = new byte[8];
                    canMsg.Len = (byte)data.Count;
                    for (var i = 0; i < canMsg.Len; i++)
                    {
                        canMsg.Data[i] = data[i];
                    }

                    canMsg.Msgtype = TpcanMessageType.PCAN_MESSAGE_STANDARD;

                    var result = PcanBasicDllWrapper.Write(this.MPcanHandle, ref canMsg);

                    if (result == TPCANStatus.PCAN_ERROR_BUSOFF)
                    {
                        this.PreparePeakCan.Reset();
                    }

                    if (result != TPCANStatus.PCAN_ERROR_OK)
                    {
                        throw new Exception("TpCanStatus.");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        #endregion

        #region Private Methods

        private void OpenCanDongle()
        {
            try
            {
                if (this.MPcanHandle == 0)
                {
                    this.MPcanHandle = this.PreparePeakCan.Do();
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