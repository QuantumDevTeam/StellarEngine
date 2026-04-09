using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Quantization;

/// <inheritdoc cref="IRegistrableMetaQuant" />
/// <typeparam name="TMeta">
/// Type of meta. Must inherit on <see cref="Stellar.Core.Quantization.MetaQuant"/>.
/// The type to define the registry to which this MetaQuant will be registered
/// This type used for <see cref="MetaQuantsRegistry{TMeta}"/>
/// </typeparam>
public abstract class RegistrableMetaQuant<TMeta>
    : MetaQuant, IRegistrableMetaQuant
    where TMeta : RegistrableMetaQuant<TMeta>
{
    /// <inheritdoc/>
    public void Register(IQuantumObject registry)
    {
        if (registry is MetaQuantsRegistry<TMeta> quantRegistry)
            quantRegistry.Register((TMeta)this);
    }

    /// <inheritdoc/>
    protected RegistrableMetaQuant(IIdentifier identifier)
        : base(identifier)
    {
        Register(MetaQuantsRegistry<TMeta>.Instance);
    }

    /// <inheritdoc/>
    public void Unregister(IQuantumObject registry)
    {
        if (registry is MetaQuantsRegistry<TMeta> quantRegistry)
            quantRegistry.Pop(UID);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Unregister(MetaQuantsRegistry<TMeta>.Instance);
    }
}