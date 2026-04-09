using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Failures;

/// <inheritdoc/>
/// <param name="sender">A Sender which create this context</param>
/// <param name="data">A data for context</param>
public readonly struct FailureContext(IQuantumObject? sender, IFailure failure, IContextData? data)
    : IFailureContext<IContextData>
{
    public IQuantumObject? Sender { get; } = sender;
    public IFailure Failure { get; } = failure;
    public IContextData? Data { get; } = data;
}