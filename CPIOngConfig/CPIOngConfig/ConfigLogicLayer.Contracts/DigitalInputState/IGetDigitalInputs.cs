using System.Collections.Generic;

namespace ConfigLogicLayer.DigitalInputState
{
    public interface IGetDigitalInputs
    {
        IReadOnlyList<bool> Get(uint node);
    }
}