using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures.Handlers;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// The central component that receives failures and routes them to appropriate handlers.
    /// </summary>
    /// <remarks>
    /// <para>The dispatcher is a quant (<see cref="IQuant"/>) that owns the failure handling pipeline.
    /// It typically uses an <see cref="IFailureHandlerProvider"/> to obtain handlers and invokes them
    /// in order until one returns <c>true</c> (stop execution) or all have been tried.</para>
    /// </remarks>
    public interface IFailureDispatcher
        : IQuant
    {
        /// <summary>
        /// Dispatches the failure described in the context to registered handlers.
        /// </summary>
        /// <param name="failureContext">The context containing the failure.</param>
        /// <returns><c>true</c> if execution should stop after handling; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// The return value is typically used by the engine to decide whether to continue normal operation,
        /// shut down a module, or terminate the entire process.
        /// </remarks>
        bool Dispatch(IFailureContext<IContextData> failureContext);
    }
}