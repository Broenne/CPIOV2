using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigLogicLayer.DigitalInputState
{
    using System.Threading;

    using Hal.PeakCan.Contracts.Basics;

    using HardwareAbstaction.PCAN.Basics;

    using Helper.Contracts.Logger;

    public class GetDigitalInputs : IGetDigitalInputs
    {
        public GetDigitalInputs(ILogger logger, IWriteBasicCan writeBasicCan, IReadCanMessage readCanMessage)
        {
            this.Logger = logger;
            this.WriteBasicCan = writeBasicCan;
            this.ReadCanMessage = readCanMessage;
        }

        private ILogger Logger { get; }

        private IWriteBasicCan WriteBasicCan { get; }

        private IReadCanMessage ReadCanMessage { get; }


        public IReadOnlyList<bool> Get(uint node)
        {
            try
            {
                this.WriteBasicCan.RemoteRequestForChannelValue(node);

                Thread.Sleep(200); // todo mb: total dämlich so


                var resFromCan = this.ReadCanMessage.Do(node);

                var result = new List<bool>();
                



                // byte 0
                for (int pos = 0; pos < 8; pos++)
                {
                    result.Add(false);
                    if ((resFromCan[0] & (1 << pos)) != 0)
                    {
                        result[pos] = true;
                        
                    }
                }

                // byte 0
                for (int pos = 0; pos < 8; pos++)
                {
                    result.Add(false);
                    if ((resFromCan[1] & (1 << pos)) != 0)
                    {
                        result[pos + 8] = true;
                    }
                }


                return result;


            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                throw;
            }
        }

    }
}
