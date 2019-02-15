using System;
using System.Collections.Generic;
using System.Text;
using Hal.PeakCan.Contracts.Basics;
using Helper.Contracts.Logger;

namespace Hal.PeakCan.Basics
{
    /// <summary>
    /// The read can message event.
    /// </summary>
    /// <seealso cref="Hal.PeakCan.Contracts.Basics.IReadCanMessageEvent" />
    public class ReadCanMessageEvent : IReadCanMessageEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCanMessageEvent"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ReadCanMessageEvent(ILogger logger)
        {
            this.Logger = logger;
        }

        

        /// <summary>
        ///     Occurs when [node is reached].
        /// </summary>
        public event EventHandler<ReadCanMessageEventArgs> EventIsReached;


        private ILogger Logger { get; }


        /// <summary>
        ///     Raises the <see cref="E:NodeReached" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ReadCanMessageEventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(ReadCanMessageEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    //return;
                    throw new Exception("Event for read can is null");
                }

                this.EventIsReached.Invoke(this, e);
            }
            catch (Exception exception)
            {
                this.Logger.LogError(exception);
                throw;
            }
            finally
            {
                this.Logger.LogEnd(this.GetType());
            }
        }


    }
}
