namespace Hal.PeakCan.Contracts.Basics
{
    using System.Collections.Generic;

    using HardwareAbstraction.Contracts.PCanDll;

    /// <summary>
    /// The interface for write basic can.
    /// </summary>
    public interface IWriteBasicCan
    {
        /// <summary>
        /// Remotes the request for channel value.
        /// </summary>
        /// <param name="node">The node info.</param>
        void RemoteRequestForChannelValue(uint node);

        /// <summary>
        /// Writes the can.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data info.</param>
        void WriteCan(uint id, IReadOnlyList<byte> data, TpcanMessageType tpcanMessageType = TpcanMessageType.PCAN_MESSAGE_STANDARD);
    }
}