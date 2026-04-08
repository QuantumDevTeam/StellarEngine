using Stellar.Core.Data.Collections;
using Stellar.Kernel;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Failures.BaseLevels;

public abstract class FailureLevel
    : IFailureLevel
{
    public IIdentifier UID { get; } = new Identifier();
    public ILabel Label { get; }
    public abstract bool IsEnabled { get; set; }
    public abstract bool IsLoggable { get; }
    public abstract bool IsStopExecute { get; }
    public abstract bool IsCritical { get; }
    public abstract bool ShouldTerminate { get; }

    private readonly IFailureDispatcherMeta _dispatcherMeta;

    public void Register(IQuantumObject registry)
    {
        if (registry is DataContainer<IFailureLevel> container)
            container.Set(this);
    }

    protected FailureLevel(string name, IFailureDispatcherMeta dispatcherMeta)
    {
        Label = new Label.Label(UID, name);
        Register((_dispatcherMeta = dispatcherMeta).FailureLevels);
    }

    public void Unregister(IQuantumObject registry)
    {
        if (registry is DataContainer<IFailureLevel> container)
            container.Pop(UID);
    }

    public void Dispose()
    {
        Unregister(_dispatcherMeta.FailureLevels);
    }
}