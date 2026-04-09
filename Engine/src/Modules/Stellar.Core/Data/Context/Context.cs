using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Context;

/// <inheritdoc/>
/// <param name="sender">A Sender which create this context</param>
/// <param name="data">A data for context</param>
public readonly struct Context<TData>(IQuantumObject? sender, TData? data)
    : IContext<TData>
    where TData : IContextData
{
    public IQuantumObject? Sender { get; } = sender;
    public TData? Data { get; } = data;
}