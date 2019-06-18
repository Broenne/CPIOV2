namespace ConfigLogicLayer.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Configurations;

    using Hal.PeakCan.Contracts.Basics;

    using HardwareAbstraction.Contracts.PCanDll;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The channel configuration service.
    /// </summary>
    /// <seealso cref="ConfigLogicLayer.Contracts.Configurations.IChannelConfiguration" />
    public class SimPulseActivate : ISimPulseActivate
    {
        private int waitForResponse = -1;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelConfiguration" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        /// <param name="channelConfigurationResponseEventHandler">The channel configuration response event handler.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        public SimPulseActivate(ILogger logger, IWriteBasicCan writeBasicCan, IChannelConfigurationResponseEventHandler channelConfigurationResponseEventHandler, IGetActualNodeId getActualNodeId)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
            this.GetActualNodeId = getActualNodeId;
            
        }

        #endregion

        #region Properties


        private IGetActualNodeId GetActualNodeId { get; }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the specified channel configuration DTO.
        /// </summary>
        /// <param name="channelConfigurationDto">The channel configuration DTO.</param>
        public Task Set()
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                
                    
                    var data = new List<byte>();
                    data.Add(0x03); // 0


                    var abholen = new List<byte>();
                    abholen.Add(0x07); // 0

                    var node = GetActualNodeId.Get();

                    return Task.Run(() =>
                    {
                        while (true)
                        {
                            // vorbereiten    
                            //Thread.Sleep(50);
                            this.WriteBasicCan.WriteCan(node, data);

                            Thread.Sleep(50);
                            this.WriteBasicCan.WriteCan(node, abholen);
                        }
                    });
                                    

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