using Stellar.Core;
using Stellar.Kernel;
using Stellar.Kernel.EventSystem;
using Stellar.Kernel.Quantization;

namespace Stellar.EventSystem.Types;

public readonly struct ModuleUnloadEventType(string moduleName)
    : IEventType
{
    public IIdentifier UID { get; } = Identifier.CreateAndRegister();
    public string Name { get; } = nameof(ModuleUnloadEventType);
    public short TypeValue { get; } = Extensions.GenerateEventTypeValue();

    public string ModuleName { get; } = moduleName;

    public void Register(IQuantumObject? registry = null)
    {
        throw new NotImplementedException();
    }

    public void Unregister(IQuantumObject? registry = null)
    {
        throw new NotImplementedException();
    }

    public void Dispose() => throw new NotImplementedException();
}