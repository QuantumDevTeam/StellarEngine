using Stellar.Kernel.Failures;

namespace Stellar.Core.Data.Context.Defaults;

public class FailureContextData(
    IFailure failure
) : IFailureContextData
{
    public IFailure Failure { get; } = failure;
}