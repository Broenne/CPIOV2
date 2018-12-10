namespace HardwareAbstaction.PCAN.Init
{
    using System;
    using System.Text;

    using Hal.PeakCan.PCANDll;

    using HardwareAbstraction.Contracts.PCanDll;

    using Helper.Contracts.Logger;

    using PCANDll;

    /// <summary>
    /// The CAN prepare service.
    /// </summary>
    /// <seealso cref="HardwareAbstraction.Contracts.Init.IPreparePeakCan" />
    public class PreparePeakCan : IPreparePeakCan //: IPreparePeakCan
    {
        private static readonly object LockInit = new object();

        private static readonly object LockDispose = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="PreparePeakCan" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="peakCanConfiguration">The peak can configuration.</param>
        public PreparePeakCan(ILogger logger)
        {

            this.MPcanHandle = 81;
            this.Baudrate = TPCANBaudrate.PCAN_BAUD_125K;
            //this.MPcanHandle = peakCanConfiguration.GetCanAdapterIdentification();
        }

       // private ILogger Logger { get; }

        private ushort MPcanHandle { get; set; } 

        private TPCANBaudrate Baudrate { get; }

        /// <summary>
        /// Does this instance.
        /// </summary>
        /// <returns>Return the handle.</returns>
        /// <exception cref="System.Exception">Throw an exception.</exception>
        public ushort Do()
        {
            try
            {
                lock (LockInit)
                {
                    var resInit = PcanBasicDllWrapper.Initialize(
                        this.MPcanHandle,
                        this.Baudrate,
                        TPCANType.PCAN_TYPE_ISA,
                        Convert.ToUInt32("0100", 16),
                        Convert.ToUInt16("3"));


                    // this.Logger.LogDebug($"Init tes {resInit}");
                    if (resInit == TPCANStatus.PCAN_ERROR_INITIALIZE)
                    {
                        return this.MPcanHandle;
                    }

                    if (resInit != TPCANStatus.PCAN_ERROR_OK)
                    {
                        // Ein Fehler ist aufgetreten. Die Rückgabewert wird in Text umgewandelt und angezeigt.
                        var strMsg = new StringBuilder(256);
                        PcanBasicDllWrapper.GetErrorText(resInit, 0, strMsg);
                        //this.Logger.LogDebug("In init :" + strMsg.ToString());
                        throw new Exception($"CAN-init not successfull handle {this.MPcanHandle}");
                    }
                      
                    //this.Logger.LogTrace("CAN is already initaized.");

                    return this.MPcanHandle;
                }
            }
            catch (Exception e)
            {
                //this.Logger.LogError(e);
                throw;
            }
        }


        public void Reset()
        {
            try
            {
                PcanBasicDllWrapper.Uninitialize(this.MPcanHandle);

                //var resInit = PcanBasicDllWrapper.Reset(this.MPcanHandle);

                //if (resInit != TPCANStatus.PCAN_ERROR_OK)
                //{
                //    // Ein Fehler ist aufgetreten. Die Rückgabewert wird in Text umgewandelt und angezeigt.
                //    var strMsg = new StringBuilder(256);
                //    PcanBasicDllWrapper.GetErrorText(resInit, 0, strMsg);
                //    Console.Write("In init :" + strMsg.ToString());
                //}

                this.Do(); // handle neu schreiben??!! todo mb. evtl das reset impliziert?

                
            }
            catch (Exception e)
            {
                //this.Logger.LogError(e);
                throw;
            }
        }





        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                lock (LockDispose)
                {
                    PcanBasicDllWrapper.Uninitialize(PcanBasicDllWrapper.PCAN_NONEBUS);
                }
            }
            catch (Exception e)
            {
                //this.Logger.LogError(e);
                throw;
            }
        }
    }
}
