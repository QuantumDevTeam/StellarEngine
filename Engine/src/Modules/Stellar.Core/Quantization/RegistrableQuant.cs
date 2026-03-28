using Stellar.Kernel.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Quantization;

/// <summary>
/// MetaQuant which auto-register in QuantsRegistry
/// </summary>
/// <remarks>Types identify which Registry be used</remarks>
/// <typeparam name="T">Type of Quant</typeparam>
/// <typeparam name="TMeta">Type of MetaQuant</typeparam>
public abstract class RegistrableQuant<T, TMeta>
    : Quant<TMeta>, IRegistrableQuant
    where TMeta : MetaQuant
    where T : RegistrableQuant<T, TMeta>
{
    /// <summary>
    /// Registration in Registry
    /// </summary>
    /// <remarks>Used in constructor, be careful!</remarks>
    public void Register(IQuantumObject registry)
    {
        if (registry is QuantsRegistry<T> quantRegistry)
            quantRegistry.Register((T)this);
    }

    /// <summary>
    /// Constructor for Quant and registration in Registry
    /// </summary>
    /// <remarks>Use registration method, be careful!</remarks>
    /// <param name="meta">MetaQuant for this Quant</param>
    protected RegistrableQuant(TMeta meta) : base(meta)
    {
        Register(QuantsRegistry<T>.Instance);
    }

    /// <summary>
    /// Unregistration ir Registry
    /// </summary>
    public void Unregister(IQuantumObject registry)
    {
        if (registry is QuantsRegistry<T> quantRegistry)
            quantRegistry.Pop(Identifier);
    }

    /// <summary>
    /// Disposing and unregistration
    /// </summary>
    /// <remarks>Use unregistration method, be careful!</remarks>
    public void Dispose()
    {
        Unregister(QuantsRegistry<T>.Instance);
    }
}