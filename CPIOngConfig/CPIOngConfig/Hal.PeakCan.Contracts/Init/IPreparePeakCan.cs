namespace HardwareAbstaction.PCAN.Init
{
    public interface IPreparePeakCan
    {
        void Dispose();
        ushort Do();
        void Reset();
    }
}