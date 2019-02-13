using System;
using System.Collections.Generic;

namespace ConfigLogicLayer.Configurations
{
    public interface ITextResponseEventHandler
    {
        event EventHandler<IReadOnlyList<byte>> EventIsReached;

        void OnReached(IReadOnlyList<byte> e);
    }
}