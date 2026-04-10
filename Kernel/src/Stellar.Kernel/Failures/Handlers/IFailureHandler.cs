using Stellar.Kernel.LoggingSystem;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures.Handlers
{
    /// <summary>
    /// Handles a specific failure by implementing custom logic.
    /// </summary>
    /// <remarks>
    /// <para>A failure handler is a registrable quant (<see cref="IRegistrableQuant"/>) that processes
    /// failures of certain types or levels. Handlers are obtained from <see cref="IFailureHandlerProvider"/>
    /// and invoked by <see cref="IFailureDispatcher"/>.</para>
    /// <para>The <see cref="Handle"/> method returns a boolean indicating whether execution should stop
    /// after handling (e.g., for critical failures).</para>
    /// </remarks>
    public interface IFailureHandler
        : IRegistrableQuant
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Gets the logger instance associated with this handler.
        /// </summary>
        ILogger Logger { get; }
#else
#nullable enable
        /// <summary>
        /// Gets the logger instance associated with this handler, or <c>null</c> if logging is unavailable.
        /// </summary>
        ILogger? Logger { get; }
#endif

        /// <summary>
        /// Handles the failure described in the context.
        /// </summary>
        /// <param name="context">The failure context containing the failure data.</param>
        /// <returns>
        /// <c>true</c> if the execution should stop after handling; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The handler can log, recover, or rethrow the failure. The return value is used by the dispatcher
        /// to decide whether to continue normal flow or terminate the current operation.
        /// </remarks>
        bool Handle(IFailureContext context);
    }
}