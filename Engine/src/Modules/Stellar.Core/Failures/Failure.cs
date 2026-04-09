using Stellar.Kernel.Failures;

namespace Stellar.Core.Failures;

public readonly struct Failure 
    : IFailure
{
    public required string Message { get; init; }
    public required Exception? InnerException { get; init; }
    public required FailureType Type { get; init; }
    public required IFailureLevel Level { get; init; }
    public required string Source { get; init; }
}