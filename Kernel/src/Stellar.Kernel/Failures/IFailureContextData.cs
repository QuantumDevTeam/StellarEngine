using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures
{
    public interface IFailureContextData
        : IContextData
    {
        IFailure Failure { get; }
    }
}