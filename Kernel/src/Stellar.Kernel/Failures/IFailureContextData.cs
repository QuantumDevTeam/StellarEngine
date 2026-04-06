using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Failure Context data
    /// </summary>
    public interface IFailureContextData
        : IContextData
    {
        /// <summary>
        /// Failure himself
        /// </summary>
        IFailure Failure { get; }
    }
}