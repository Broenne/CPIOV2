namespace ConfigLogicLayer.Analog
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using ConfigLogicLayer.Contracts;
    using ConfigLogicLayer.Contracts.ActualId;
    using ConfigLogicLayer.Contracts.Analog;

    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    /// <summary>
    ///     The service for analog can.
    /// </summary>
    public class AnalogCan : IAnalogCan
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnalogCan" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="getActualNodeId">The get actual node identifier.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public AnalogCan(ILogger logger, IGetActualNodeId getActualNodeId, IWriteBasicCan writeBasicCan)
        {
            this.Logger = logger;
            this.GetActualNodeId = getActualNodeId;
            this.WriteBasicCan = writeBasicCan;
        }

        #endregion

        #region Properties

        private CancellationTokenSource CancelToken { get; set; }

        private IGetActualNodeId GetActualNodeId { get; }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            try
            {
                this.CancelToken?.Cancel();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        ///     Triggers the specified channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void Trigger(uint channel)
        {
            try
            {
                var nodeId = this.GetActualNodeId.Get();
                var cobId = Convert.ToUInt32(nodeId + CanCommandConsts.RequestAnalogValue);
                var data = new List<byte>();
                data.Add(Convert.ToByte(channel));

                this.WriteBasicCan.WriteCan(cobId, data);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        ///     Triggers the run all.
        /// </summary>
        /// <returns>Return the task.</returns>
        public Task TriggerRunAll()
        {
            try
            {
                this.CancelToken = new CancellationTokenSource();
                var tsk = Task.Run(
                    () =>
                        {
                            while (!this.CancelToken.IsCancellationRequested)
                            {
                                for (uint i = 0; i < 16; i++)
                                {
                                    this.Trigger(i);
                                    Thread.Sleep(100);
                                }
                            }
                        });

                return tsk;
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