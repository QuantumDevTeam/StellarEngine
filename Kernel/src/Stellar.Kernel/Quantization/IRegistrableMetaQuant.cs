namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// Combines <see cref="IMetaQuant"/> with registration capabilities.
    /// </summary>
    /// <remarks>
    /// <para>Some metadata objects need to be discoverable via registries – for example, to look up all meta
    /// of a certain type or to manage global metadata collections.</para>
    /// <para>This interface is typically implemented by metadata that is not tied to a single quant’s lifetime
    /// or that needs to be shared across different parts of the engine.</para>
    /// </remarks>
    public interface IRegistrableMetaQuant
        : IMetaQuant, IRegistrableQuantumObject
    {
    }
}