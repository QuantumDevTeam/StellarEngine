using Stellar.Kernel.EntryPoint;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Context.Defaults;

public class StopContextData(
    StopReason reason,
    IQuantumObject? data = null
) : IStopContextData
{
    public StopReason Reason { get; } = reason;
    public IQuantumObject? Data { get; } = data;
}