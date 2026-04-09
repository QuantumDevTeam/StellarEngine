namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// The absolute base marker interface for any type that participates in the Engine’s quantization system.
    /// </summary>
    /// <remarks>
    /// <para>"Quantization" in Stellar refers to the ability to break down engine and game objects into discrete,
    /// manageable units that can be serialized, replicated, and garbage-collected in a controlled manner.</para>
    /// <para>All core engine abstractions (identifiers, tasks, contexts, etc.) derive from this interface,
    /// either directly or indirectly. It has no members by design – it only signals that the implementing type
    /// is part of the quantized world.</para>
    /// </remarks>
    public interface IQuantumObject
    {
    }
}