using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.Logging;

namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Context of running any Module, of any Entrypoint
    /// </summary>
    public interface IModuleRunContextData
        : IContextData
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Context logger
        /// </summary>
        ILogger Logger { get; }
        
        /// <summary>
        /// Module work directory
        /// </summary>
        ILocation WorkDirectory { get; }
        
        /// <summary>
        /// Dispatcher for this context
        /// </summary>
        IFailureDispatcher FailureDispatcher { get; }

        /// <summary>
        /// Run optional custom data
        /// </summary>
        IQuantumObject InitData { get; }
#else
#nullable enable
        /// <summary>
        /// Context logger
        /// </summary>
        ILogger? Logger { get; }

        /// <summary>
        /// Module work directory
        /// </summary>
        ILocation? WorkDirectory { get; }

        /// <summary>
        /// Dispatcher for this context
        /// </summary>
        IFailureDispatcher? FailureDispatcher { get; }

        /// <summary>
        /// Run optional custom data
        /// </summary>
        IQuantumObject? InitData { get; }
#endif
    }
}