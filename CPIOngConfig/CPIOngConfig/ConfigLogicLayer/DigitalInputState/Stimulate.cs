using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigLogicLayer.DigitalInputState
{
    using Hal.PeakCan.Contracts.Basics;

    using Helper.Contracts.Logger;

    public class Stimulate : IStimulate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stimulate"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="writeBasicCan">The write basic can.</param>
        public Stimulate(ILogger logger, IWriteBasicCan writeBasicCan)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
        }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        /// <summary>
        /// Requests the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RequestById(uint id)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());

                this.WriteBasicCan.RemoteRequestForChannelValue(id);
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

    }
}
