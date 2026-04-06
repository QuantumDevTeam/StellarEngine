using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Logging;

namespace Stellar.Kernel.Failures.Handlers
{
    /// <summary>
    /// Handler of a Failure
    /// </summary>
    public interface IFailureHandler
        : IRegistrableQuant
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Handler logger
        /// </summary>
        ILogger Logger { get; }
#else
#nullable enable
        /// <summary>
        /// Handler logger
        /// </summary>
        ILogger? Logger { get; }
#endif

        /// <summary>
        /// Handler method
        /// </summary>
        /// <param name="context">Context of Failure</param>
        /// <returns>Is stop execution</returns>
        bool Handle(IContext<IFailureContextData> context);
    }
}