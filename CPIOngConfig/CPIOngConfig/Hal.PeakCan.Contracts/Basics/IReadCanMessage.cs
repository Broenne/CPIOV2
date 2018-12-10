namespace HardwareAbstaction.PCAN.Basics
{
    public interface IReadCanMessage
    {
        byte[] Do(uint id);
    }
}