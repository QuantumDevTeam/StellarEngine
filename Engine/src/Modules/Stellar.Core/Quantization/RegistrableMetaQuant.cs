using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Quantization;

/// <summary>
/// MetaQuant which auto-register in MetaQuantsRegistry
/// </summary>
/// <remarks>Type identify which Registry be used</remarks>
/// <typeparam name="TMeta">Type of MetaQuant</typeparam>
public abstract class RegistrableMetaQuant<TMeta>
    : MetaQuant, IRegistrableMetaQuant
    where TMeta : RegistrableMetaQuant<TMeta>
{
    /// <summary>
    /// Registration in Registry
    /// </summary>
    /// <remarks>Used in constructor, be careful!</remarks>
    public void Register(IQuantumObject registry)
    {
        if (registry is MetaQuantsRegistry<TMeta> quantRegistry)
            quantRegistry.Register((TMeta)this);
    }

    /// <summary>
    /// Constructor for MetaQuant and registration in Registry
    /// </summary>
    /// <remarks>Use registration method, be careful!</remarks>
    /// <param name="identifier">Quant Identifier</param>
    protected RegistrableMetaQuant(IIdentifier? identifier = null)
        : base(identifier)
    {
        Register(MetaQuantsRegistry<TMeta>.Instance);
    }

    /// <summary>
    /// Unregistration ir Registry
    /// </summary>
    public void Unregister(IQuantumObject registry)
    {
        if (registry is MetaQuantsRegistry<TMeta> quantRegistry)
            quantRegistry.Pop(UID);
    }

    /// <summary>
    /// Disposing and unregistration
    /// </summary>
    /// <remarks>Use unregistration method, be careful!</remarks>
    public void Dispose()
    {
        Unregister(MetaQuantsRegistry<TMeta>.Instance);
    }
}