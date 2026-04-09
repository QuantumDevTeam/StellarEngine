using Stellar.Kernel.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Quantization;

/// <inheritdoc cref="IRegistrableQuant" />
/// <typeparam name="T">
/// the type to define the registry to which this Quant will be registered
/// This type used for <see cref="MetaQuantsRegistry{TMeta}"/>
/// </typeparam>
/// <typeparam name="TMeta">Type of meta. Must inherit on <see cref="Stellar.Core.Quantization.MetaQuant"/>.</typeparam>
public abstract class RegistrableQuant<T, TMeta>
    : Quant<TMeta>, IRegistrableQuant
    where TMeta : MetaQuant
    where T : RegistrableQuant<T, TMeta>
{
    /// <inheritdoc/>
    public void Register(IQuantumObject registry)
    {
        if (registry is QuantsRegistry<T> quantRegistry)
            quantRegistry.Register((T)this);
    }

    /// <inheritdoc/>
    protected RegistrableQuant(TMeta meta) 
        : base(meta)
    {
        Register(QuantsRegistry<T>.Instance);
    }

    /// <inheritdoc/>
    public void Unregister(IQuantumObject registry)
    {
        if (registry is QuantsRegistry<T> quantRegistry)
            quantRegistry.Pop(UID);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Unregister(QuantsRegistry<T>.Instance);
    }
}