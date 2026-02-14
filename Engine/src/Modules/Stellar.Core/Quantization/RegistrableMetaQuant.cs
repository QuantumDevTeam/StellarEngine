using Stellar.Core.Data.Registry;
using Stellar.Kernel;

namespace Stellar.Core.Quantization;

public abstract class RegistrableMetaQuant<T>
    : MetaQuant, IDisposable
    where T : RegistrableMetaQuant<T>
{
    public void RegisterThisMetaQuant()
    {
        MetaQuantsRegistry<T>.Instance.Register((T)this);
    }

    protected RegistrableMetaQuant(IIdentifier? identifier = null) : base(identifier)
    {
        RegisterThisMetaQuant();
    }

    public void UnregisterThisMetaQuant()
    {
        MetaQuantsRegistry<T>.Instance.Pop(Identifier);
    }

    public void Dispose()
    {
        UnregisterThisMetaQuant();
    }
}