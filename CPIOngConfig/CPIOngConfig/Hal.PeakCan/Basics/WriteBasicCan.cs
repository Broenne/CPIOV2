namespace Hal.PeakCan.Basics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

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
    public class WriteBasicCan : IWriteBasicCan //: IWriteBasicCan
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

            //locksWrite = new Dictionary<uint, object>();
            //for (uint i=0;i<(256);i++)
            //{
            //    locksWrite.Add(i, new object());
            //}
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

        ///// <summary>
        /////     Creates the SDO request.
        ///// </summary>
        ///// <param name="node">The node info.</param>
        ///// <param name="data">The data field.</param>
        //public void CreateSdoRequest(uint node, byte[] data)
        //{
        //    this.SendWithData(node, data, TpcanMessageType.PCAN_MESSAGE_STANDARD);
        //}

        ///// <summary>
        /////     Remotes the request for channel value.
        ///// </summary>
        ///// <param name="node">The node value.</param>
        //public void RemoteRequestExtended(uint node)
        //{
        //    try
        //    {
        //        var canMsg = new TpcanMsg
        //                         {
        //                             Id = node,
        //                             Msgtype = TpcanMessageType.PCAN_MESSAGE_RTR
        //                                       | TpcanMessageType.PCAN_MESSAGE_EXTENDED
        //                         };

        //        // Console.WriteLine("canMsg.ID: RemoteRequest" + canMsg.ID);
        //        var result = PcanBasicDllWrapper.Write(this.MPcanHandle, ref canMsg);

        //        if (result != TPCANStatus.PCAN_ERROR_OK)
        //        {
        //            throw new Exception("TpCanStatus");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        this.Logger.LogError(e);
        //    }
        //}

        /// <summary>
        ///     Remotes the request for channel value.
        /// </summary>
        /// <param name="node">The node value.</param>
        public void RemoteRequestForChannelValue(uint node)
        {
            try
            {


                ClearMessageBuffer(node);

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



        /// <summary>
        ///     Clears the message buffer.
        /// </summary>
        /// <param name="type">The type of message.</param>
        public void ClearMessageBuffer(uint id, TpcanMessageType type = TpcanMessageType.PCAN_MESSAGE_STANDARD)
        {
            TpcanMsg readCanMsg;
            readCanMsg.Id = 0x1B;
            readCanMsg.Len = 8;

            readCanMsg.Msgtype = type;

            while (TPCANStatus.PCAN_ERROR_QRCVEMPTY
                   != PcanBasicDllWrapper.Read(this.MPcanHandle, out readCanMsg, out _))
            {
                // Console.WriteLine($"clear {readCanMsg.Id}");
                this.Logger.LogTrace("read empty" + readCanMsg.Id);
            }
        }





        /// <summary>
        ///     Sends the extended message.
        /// </summary>
        /// <param name="node">The node info.</param>
        /// <param name="data">The data field.</param>
        public void SendExtendedMessage(uint node, byte[] data)
        {
            this.SendWithData(node, data, TpcanMessageType.PCAN_MESSAGE_EXTENDED);
        }

        /// <summary>
        ///     Writes the can and check.
        /// </summary>
        /// <param name="canMsg">The can MSG.</param>
        /// <exception cref="System.Exception">TP Can Status.</exception>
        public void WriteCanAndCheck(TpcanMsg canMsg)
        {
            lock (LockWrite)
                
            {
                this.Logger.LogBegin(this.GetType());

                
                var result = PcanBasicDllWrapper.Write(this.MPcanHandle, ref canMsg);

                //Console.WriteLine("b" + ByteArrayToHexString(canMsg.Data));
                //Thread.Sleep(1);
                //PcanBasicDllWrapper.SetValue(this.MPcanHandle, TpcanParameter..PCAN_INTERFRAME_DELAY)

                if (result == TPCANStatus.PCAN_ERROR_BUSOFF)
                {
                    this.PreparePeakCan.Reset();
                }

                if (result != TPCANStatus.PCAN_ERROR_OK)
                {
                    //PcanBasicDllWrapper.Uninitialize(this.MPcanHandle);

                    //this.PreparePeakCan.Reset();
                    //this.WriteCanAndCheck(canMsg);
                    //Thread.Sleep(50);
                    //this.Logger.LogDebug($"{DateTime.Now.ToString(HelperStrings.TimeString)} {result.ToString()}");
                    //throw new Exception("TpCanStatus");
                }
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

        [SuppressMessage(
            "StyleCop.CSharp.NamingRules",
            "SA1305:FieldNamesMustNotUseHungarianNotation",
            Justification = "Reviewed. Suppression is OK here.")]
        private void SendWithData(uint node, byte[] data, TpcanMessageType tPcanMessageType)
        {
            var canMsg = new TpcanMsg();

            canMsg.Msgtype = tPcanMessageType;
            canMsg.Id = node;
            canMsg.Data = new byte[8];

            if (data != null)
            {
                for (var i = 0; i < data.Length; i++)
                {
                    canMsg.Data[i] = data[i];
                }
            }

            canMsg.Len = 8;

            // this.Logger.LogDebug("canMsg.ID: RemoteRequest" + canMsg.Id);
            this.WriteCanAndCheck(canMsg);
        }

        #endregion
    }
}