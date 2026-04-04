using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.Logging;

namespace Stellar.Kernel.EntryPoint
{
    public interface IModuleRunContextData
        : IContextData
    {
#if NETSTANDARD2_0
        ILogger Logger { get; }
        ILocation WorkDirectory { get; }
        IFailureDispatcher FailureDispatcher { get; }

        // other
        IQuantumObject InitData { get; }
#else
#nullable enable
        ILogger? Logger { get; }
        ILocation? WorkDirectory { get; }
        IFailureDispatcher? FailureDispatcher { get; }

        // other
        IQuantumObject? InitData { get; }
#endif
    }
}