using Stellar.Core.Data.Registry;

namespace Stellar.Core.Quantization;

public abstract class RegistrableQuant<T, TMeta>
    : Quant<TMeta>, IDisposable
    where T : RegistrableQuant<T, TMeta>
    where TMeta : MetaQuant
{
    public void RegisterThisQuant()
    {
        QuantsRegistry<T, TMeta>.Instance.Register((T)this);
    }

    protected RegistrableQuant(TMeta meta) : base(meta)
    {
        RegisterThisQuant();
    }

    public void UnregisterThisQuant()
    {
        QuantsRegistry<T, TMeta>.Instance.Pop(Identifier);
    }

    public void Dispose()
    {
        UnregisterThisQuant();
    }
}