using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Context data that carries a specific failure instance.
    /// </summary>
    /// <remarks>
    /// This data is used with <see cref="IContext{TData}"/> when dispatching failures to handlers.
    /// </remarks>
    public interface IFailureContext<out T>
        : IContext<T>
        where T : IContextData
    {
        /// <summary>
        /// Gets the failure being processed.
        /// </summary>
        IFailure Failure { get; }
    }
}