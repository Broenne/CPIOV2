namespace Hal.PeakCan.Contracts.Basics
{
    public interface IWriteBasicCan
    {
        void RemoteRequestForChannelValue(uint node);
    }
}