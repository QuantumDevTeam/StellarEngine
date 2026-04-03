using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Context;

public class Context<TData>(IQuantumObject? sender, TData? data)
    : IContext<TData>
    where TData : IContextData
{
    public IQuantumObject? Sender { get; } = sender;
    public TData? Data { get; } = data;
}