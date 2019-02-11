namespace Hal.PeakCan.Contracts.Init
{
    /// <summary>
    /// The service for prepare peak ca.
    /// </summary>
    public interface IPreparePeakCan
    {
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Does this instance.
        /// </summary>
        /// <returns></returns>
        ushort Do();

        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset();
    }
}