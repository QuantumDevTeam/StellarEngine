using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Context;

/// <inheritdoc/>
/// <param name="sender">A Sender which create this context</param>
/// <param name="data">A data for context</param>
public readonly struct Context(IQuantumObject? sender, IContextData? data)
    : IContext
{
    public IQuantumObject? Sender { get; } = sender;
    public IContextData? RawData { get; } = data;
    public T? GetData<T>() where T : struct, IContextData => (T?)RawData;
}