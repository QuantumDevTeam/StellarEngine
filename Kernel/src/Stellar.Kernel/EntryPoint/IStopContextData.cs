using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Context data passed to <see cref="StellarEntryPoint.RequestStop"/>.
    /// </summary>
    /// <remarks>
    /// Contains the reason for stopping and optional custom data.
    /// </remarks>
    public interface IStopContextData
        : IContextData
    {
        /// <summary>
        /// Gets the reason why the stop was requested.
        /// </summary>
        StopReason Reason { get; }

#if NETSTANDARD2_0
        /// <summary>
        /// Gets optional custom data associated with the stop request.
        /// </summary>
        IQuantumObject Data { get; }
#else
#nullable enable
        /// <summary>
        /// Gets optional custom data associated with the stop request.
        /// </summary>
        IQuantumObject? Data { get; }
#endif
    }
}