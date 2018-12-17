namespace Hal.PeakCan.Basics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Hal.PeakCan.Contracts.Basics;
    using Hal.PeakCan.PCANDll;

    using HardwareAbstaction.PCAN.Init;
    using HardwareAbstaction.PCAN.PCANDll;

    using HardwareAbstraction.Contracts.PCanDll;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The write basic CAN class.
    /// </summary>
    /// <seealso cref="HardwareAbstraction.Contracts.Basics.IWriteBasicCan" />
    public class WriteBasicCan : IWriteBasicCan
    {
        private static readonly object LockWrite = new object();

        private static int cnt;

        private static Dictionary<uint, object> locksWrite;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="WriteBasicCan" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="preparePeakCan">The prepare peak can.</param>
        [SuppressMessage(
            "StyleCop.CSharp.NamingRules",
            "SA1305:FieldNamesMustNotUseHungarianNotation",
            Justification = "Reviewed. Suppression is OK here.")]
        public WriteBasicCan( IPreparePeakCan preparePeakCan, ILogger logger)
        {
            this.PreparePeakCan = preparePeakCan;
            this.Logger = logger;

            try
            {
                this.MPcanHandle = preparePeakCan.Do();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        ///     Gets the actual handle.
        /// </summary>
        /// <returns>Return the actual handle.</returns>
        public string GetActualHandle()
        {
            return this.MPcanHandle.ToString("X"); // todo mb: hier namen rein bauen?!
        }

        #endregion

        #region Properties

        private ILogger Logger { get; }

        private ushort MPcanHandle { get; }

        private IPreparePeakCan PreparePeakCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Remotes the request for channel value.
        /// </summary>
        /// <param name="node">The node value.</param>
        public void RemoteRequestForChannelValue(uint node)
        {
            try
            {


                //ClearMessageBuffer(node);

                var canMsg = new TpcanMsg
                                 {
                                     Id = node,
                                     Msgtype = TpcanMessageType.PCAN_MESSAGE_RTR,
                                     Data = new byte[8],
                                     Len =
                                         2 // todo mb: die längenangabe ist aufgrund eines Fehlverhaltens im alten Knoten notwendig
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
        //public void ClearMessageBuffer(uint id, TpcanMessageType type = TpcanMessageType.PCAN_MESSAGE_STANDARD)
        //{
        //    TpcanMsg readCanMsg;
        //    readCanMsg.Id = 0x1B;
        //    readCanMsg.Len = 8;

        //    readCanMsg.Msgtype = type;

        //    while (TPCANStatus.PCAN_ERROR_QRCVEMPTY
        //           != PcanBasicDllWrapper.Read(this.MPcanHandle, out readCanMsg, out _))
        //    {
        //        // Console.WriteLine($"clear {readCanMsg.Id}");
        //        this.Logger.LogTrace("read empty" + readCanMsg.Id);
        //    }
        //}







        /// <summary>
        /// Writes the can.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="Exception">TpCanStatus</exception>
        public void WriteCan(uint id, IReadOnlyList<byte> data)
        {
            try
            {
                lock (LockWrite)
                {
                    this.Logger.LogBegin(this.GetType());

                    TpcanMsg canMsg = new TpcanMsg();
                    canMsg.Id = id;
                    canMsg.Data = data.ToArray();
                    canMsg.Len = (byte)data.Count;
                    canMsg.Msgtype = TpcanMessageType.PCAN_MESSAGE_STANDARD;

                    var result = PcanBasicDllWrapper.Write(this.MPcanHandle, ref canMsg);


                    if (result == TPCANStatus.PCAN_ERROR_BUSOFF)
                    {
                        this.PreparePeakCan.Reset();
                    }

                    if (result != TPCANStatus.PCAN_ERROR_OK)
                    {
                        throw new Exception("TpCanStatus");
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

        private string ByteArrayToHexString(byte[] data)
        {
            var msg = string.Empty;
            foreach (var item in data)
            {
                msg += item.ToString("x2");
            }

            return msg;
        }

       

        #endregion
    }
}