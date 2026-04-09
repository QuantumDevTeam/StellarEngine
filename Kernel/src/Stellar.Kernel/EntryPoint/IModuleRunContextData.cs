using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.Logging;

namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Context data passed to <see cref="StellarEntryPoint.Run"/> when a module is started.
    /// </summary>
    /// <remarks>
    /// <para>Provides the entry point with essential services: logging, file system access, failure handling,
    /// and optional initialization data.</para>
    /// <para>All properties are optional (nullable) because the engine may not provide them in every environment.</para>
    /// </remarks>
    public interface IModuleRunContextData
        : IContextData
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Gets the logger instance for the context.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Gets the working directory location for the module.
        /// </summary>
        ILocation WorkDirectory { get; }

        /// <summary>
        /// Gets the failure dispatcher used to report errors and exceptions.
        /// </summary>
        IFailureDispatcher FailureDispatcher { get; }

        /// <summary>
        /// Gets optional custom initialization data.
        /// </summary>
        IQuantumObject InitData { get; }
#else
#nullable enable
        /// <summary>
        /// Gets the logger instance for the context, or <c>null</c> if logging is not available.
        /// </summary>
        ILogger? Logger { get; }

        /// <summary>
        /// Gets the working directory location for the module, or <c>null</c> if not set.
        /// </summary>
        ILocation? WorkDirectory { get; }

        /// <summary>
        /// Gets the failure dispatcher, or <c>null</c> if failure handling is disabled.
        /// </summary>
        IFailureDispatcher? FailureDispatcher { get; }

        /// <summary>
        /// Gets optional custom initialization data, or <c>null</c>.
        /// </summary>
        IQuantumObject? InitData { get; }
#endif
    }
}