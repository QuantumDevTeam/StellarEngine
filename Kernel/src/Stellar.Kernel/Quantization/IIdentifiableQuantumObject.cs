using Stellar.Kernel.Label;

namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// Extends <see cref="IQuantumObject"/> with a unique identifier, making the object trackable across the engine.
    /// </summary>
    /// <remarks>
    /// <para>Any quantized object that needs to be uniquely referenced (e.g., tasks, entities, resources)
    /// should implement this interface. The identifier is typically immutable and assigned at creation.</para>
    /// <para>This is the base for <see cref="IMetaQuant"/>, <see cref="IQuant"/>, and <see cref="ILabeled"/>.</para>
    /// </remarks>
    public interface IIdentifiableQuantumObject
        : IQuantumObject
    {
        /// <summary>
        /// Gets the unique identifier of this object.
        /// </summary>
        /// <value>An <see cref="IIdentifier"/> instance that uniquely represents the object.</value>
        /// <remarks>
        /// The <see cref="UID"/> must be stable for the lifetime of the object.
        /// It is used for dependency resolution, lookups, and serialization.
        /// </remarks>
        IIdentifier UID { get; }
    }
}