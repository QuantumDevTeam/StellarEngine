using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Failures;

/// <inheritdoc/>
/// <param name="sender">A Sender which create this context</param>
/// <param name="data">A data for context</param>
public readonly struct FailureContext(IQuantumObject? sender, IFailure failure, IContextData? data)
    : IFailureContext
{
    public IQuantumObject? Sender { get; } = sender;
    public IContextData? RawData { get; } = data;
    public IFailure Failure { get; } = failure;

    public T? GetData<T>() where T : struct, IContextData => (T?)RawData;
}