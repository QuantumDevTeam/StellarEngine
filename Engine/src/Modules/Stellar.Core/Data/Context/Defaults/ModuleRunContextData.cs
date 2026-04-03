using Stellar.Kernel.EntryPoint;
using Stellar.Kernel.Failures;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.Logging;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Context.Defaults;

public class ModuleRunContextData(
    ILogger? logger,
    ILocation? workDirectory,
    IFailureDispatcher? failureDispatcher,
    IQuantumObject? initData = null
) : IModuleRunContextData
{
    public ILogger? Logger { get; } = logger;
    public ILocation? WorkDirectory { get; } = workDirectory;
    public IFailureDispatcher? FailureDispatcher { get; } = failureDispatcher;
    public IQuantumObject? InitData { get; } = initData;
}