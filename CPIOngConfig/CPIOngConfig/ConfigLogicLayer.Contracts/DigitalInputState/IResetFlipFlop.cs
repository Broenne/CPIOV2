namespace ConfigLogicLayer.Contracts.DigitalInputState
{
    public interface IResetFlipFlop
    {
        void ResetAll();

        void ResetChannel(uint channel);
    }
}