using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EntryPoint
{
    public interface IStopContextData : IContextData
    {
        StopReason Reason { get; }
#if NETSTANDARD2_0
        IQuantumObject Data { get; }
#else
#nullable enable
        IQuantumObject? Data { get; }
#endif
    }
}