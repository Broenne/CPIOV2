using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigLogicLayer.Text
{
    using ConfigLogicLayer.Configurations;

    using Helper.Contracts.Logger;

    public class Text
    {

        private ILogger Logger { get; }

        private ITextResponseEventHandler TextResponseEventHandler { get; }

        public void Request(uint position)
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }


    }
}
