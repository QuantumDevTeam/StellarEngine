using Stellar.Kernel;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Failures.BaseLevels;

public abstract class FailureLevel : IFailureLevel, ILabeled
{
    public abstract string Name { get; }
    public abstract bool IsEnabled { get; set; }
    public abstract bool IsLoggable { get; }
    public abstract bool IsStopExecute { get; }
    public abstract bool IsCritical { get; }
    public abstract bool ShouldTerminate { get; }

    public void Register(IQuantumObject registry)
    {
        // TODO: implement registration of failure level
        throw new NotImplementedException();
    }

    public void Unregister(IQuantumObject registry)
    {
        // TODO: implement unregistration of failure level
        throw new NotImplementedException();
    }
}