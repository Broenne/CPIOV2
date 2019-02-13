using System;

namespace CPIOngConfig.CanText
{
    public interface ITextForDisplayEventHandler
    {
        event EventHandler<string> EventIsReached;

        void OnReached(string e);
    }
}