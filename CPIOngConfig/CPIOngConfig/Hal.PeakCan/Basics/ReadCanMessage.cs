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
        public ReadCanMessage(ILogger logger, IPreparePeakCan preparePeakCan)
        {
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

        //#endregion

        //#region Public Methods

        ///// <summary>
        /////     Clears the message buffer.
        ///// </summary>
        //public void ClearExtendedMessageBuffer()
        //{
        //    this.ClearMessageBuffer(TpcanMessageType.PCAN_MESSAGE_EXTENDED);
        //}

        ///// <summary>
        /////     Clears the message buffer.
        ///// </summary>
        ///// <param name="type">The type of message.</param>
        //public void ClearMessageBuffer(TpcanMessageType type = TpcanMessageType.PCAN_MESSAGE_STANDARD)
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
        ///     Reads the can MSG.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     Return the CAN message.
        /// </returns>
        /// <exception cref="DigitalReadException">Throw digital read exception.</exception>
        /// <exception cref="System.Exception">Answer not correct, necessary to log.</exception>
        [SuppressMessage(
            "StyleCop.CSharp.NamingRules",
            "SA1305:FieldNamesMustNotUseHungarianNotation",
            Justification = "Reviewed. Suppression is OK here.")]
        public byte[] Do(uint id)
        {
            lock (LockRead)
            {
                try
                {
                    TpcanMsg readCanMsg;
                    TPCANStatus res;
                    this.ReadRaw(id, out readCanMsg /*, out res*/);

                    if (readCanMsg.Id == id)
                    {
                        this.Logger.LogDebug("GEHT#################");
                    }
                    else
                    {
                        throw new Exception("antwort passt nicht, muss man das locken?");
                    }

                    this.Logger.LogDebug("post read readCanMsg.Id: " + readCanMsg.Id);
                    //this.Logger.LogDebug("res: " + res);

                    return readCanMsg.Data;
                }
                catch (Exception e)
                {
                    this.Logger.LogError(e);
                    throw;
                }
            }
        }

        ///// <summary>
        /////     Gets the actual handle.
        ///// </summary>
        ///// <returns>Return the actual handle.</returns>
        //public string GetActualHandle()
        //{
        //    return this.MPcanHandle.ToString("X"); // todo mb: hier namen rein bauen?!
        //}

        ///// <summary>
        /////     Tries the get value.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="data">The data to read.</param>
        ///// <returns>Return if result ok.</returns>
        //public bool TryGetValue(uint id, uint datat1, uint data2, ref byte[] data)
        //{
        //    lock (LockRead)
        //    {
        //        this.Logger.LogTrace("Begin :" + this.GetType().Name);

        //        try
        //        {
        //            TpcanMsg readCanMsg;
        //            this.ReadRaw(id, out readCanMsg, datat1, data2);

        //            data = readCanMsg.Data;

        //            if (readCanMsg.Id == id && readCanMsg.Data[0] == datat1 && readCanMsg.Data[1] == data2)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            this.Logger.LogError(e);
        //            throw;
        //        }
        //        finally
        //        {
        //            this.Logger.LogTrace("End :" + this.GetType().Name);
        //        }
        //    }
        //}

        ///// <summary>
        /////     Tries the get value.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="data">The data to read.</param>
        ///// <returns>Return if result ok.</returns>
        //public bool TryGetValue(uint id, ref byte[] data)
        //{
        //    lock (LockRead)
        //    {
        //        this.Logger.LogTrace("Begin :" + this.GetType().Name);

        //        try
        //        {
        //            TpcanMsg readCanMsg;
        //            this.ReadRaw(id, out readCanMsg);

        //            data = readCanMsg.Data;

        //            if (readCanMsg.Id == id)
        //            {
        //                // this.Logger.LogTrace("GEHT#################" + Encoding.Default.GetString(data));
        //                return true;
        //            }
        //            else
        //            {
        //                // this.Logger.LogTrace("Bus status" + status);
        //                return false;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            this.Logger.LogError(e);
        //            throw;
        //        }
        //        finally
        //        {
        //            this.Logger.LogTrace("End :" + this.GetType().Name);
        //        }
        //    }
        //}

        //#endregion

        //#region Private Methods

        private void ReadRaw(uint id, out TpcanMsg readCanMsg)
        {
            //this.Logger.LogTrace("Begin :" + this.GetType().Name + $"  {DateTime.Now.ToString(HelperStrings.TimeString)}");

            try
            {
                TPCANStatus result;

                
                // Console.WriteLine($"in raw  { DateTime.Now.ToString(HelperStrings.TimeString)}");
                do
                {
                    PcanBasicDllWrapper.FilterMessages(this.MPcanHandle, id, id, TpcanMode.PCAN_MODE_STANDARD);
                    result = PcanBasicDllWrapper.Read(this.MPcanHandle, out readCanMsg, out _);
                    if (result == TPCANStatus.PCAN_ERROR_OK)
                    {
                        break;
                    }
                }
                while ((result & TPCANStatus.PCAN_ERROR_QRCVEMPTY) != TPCANStatus.PCAN_ERROR_QRCVEMPTY);

                ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //        // Console.WriteLine($"out raw  { DateTime.Now.ToString(HelperStrings.TimeString)}");
        //    }
        //    catch (Exception e)
        //    {
        //        this.Logger.LogError(e);
        //        throw;
        //    }
        //    finally
        //    {
        //        this.Logger.LogTrace(
        //            "End :" + this.GetType().Name + $"  {DateTime.Now.ToString(HelperStrings.TimeString)}");
        //    }
        //}

                //#endregion

    }
}