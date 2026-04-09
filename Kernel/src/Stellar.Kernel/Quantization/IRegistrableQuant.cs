namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// Represents a <see cref="IQuant"/> that can be registered in a registry for global access.
    /// </summary>
    /// <remarks>
    /// <para>Most active engine objects (entities, systems, tasks) are registrable quants.
    /// Registration allows the engine to manage them uniformly – for iteration, serialization, or network replication.</para>
    /// <para>When a registrable quant is destroyed, it should automatically unregister itself from all registries
    /// to avoid dangling references.</para>
    /// </remarks>
    public interface IRegistrableQuant
        : IQuant, IRegistrableQuantumObject
    {
    }
}