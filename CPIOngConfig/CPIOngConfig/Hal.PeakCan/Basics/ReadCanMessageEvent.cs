using System;
using System.Collections.Generic;
using System.Text;
using Hal.PeakCan.Contracts.Basics;
using Helper.Contracts.Logger;

namespace Hal.PeakCan.Basics
{
    public class ReadCanMessageEvent : IReadCanMessageEvent
    {
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
        /// <param name="e">The <see cref="NodeEventArgs" /> instance containing the event data.</param>
        public virtual void OnReached(ReadCanMessageEventArgs e)
        {
            try
            {
                this.Logger.LogBegin(this.GetType());
                if (this.EventIsReached == null)
                {
                    throw new Exception("Event for google is null");
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
