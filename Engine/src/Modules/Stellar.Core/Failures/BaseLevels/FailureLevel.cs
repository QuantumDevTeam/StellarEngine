using Stellar.Kernel.Failures;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Failures.BaseLevels;

public abstract class FailureLevel : IFailureLevel, ILabeled
{
    public abstract ILabel Label { get; }
    public abstract bool IsEnabled { get; set; }
    public abstract bool IsLoggable { get; }
    public abstract bool IsStopExecute { get; }
    public abstract bool IsCritical { get; }
    public abstract bool ShouldTerminate { get; }

    public void Register(IQuantumObject registry)
    {
        throw new NotImplementedException();
    }

    public void Unregister(IQuantumObject registry)
    {
        throw new NotImplementedException();
    }
}