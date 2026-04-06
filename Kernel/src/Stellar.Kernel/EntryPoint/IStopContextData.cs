using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Context of stopping execution
    /// </summary>
    public interface IStopContextData
        : IContextData
    {
        /// <summary>
        /// Stop reason
        /// </summary>
        StopReason Reason { get; }
#if NETSTANDARD2_0
        /// <summary>
        /// Stop optional custom data
        /// </summary>
        IQuantumObject Data { get; }
#else
#nullable enable
        /// <summary>
        /// Stop optional custom data
        /// </summary>
        IQuantumObject? Data { get; }
#endif
    }
}