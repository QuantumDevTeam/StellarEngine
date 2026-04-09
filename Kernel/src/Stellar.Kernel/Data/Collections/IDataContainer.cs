using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Collections
{
    /// <summary>
    /// Represents a quantum object that can contain and manage other identifiable quantum objects.
    /// </summary>
    /// <remarks>
    /// <para>A data container is a registrable quant (<see cref="IRegistrableQuant"/>) that acts as a collection
    /// for objects implementing <see cref="IIdentifiableQuantumObject"/>. It supports lookup by identifier
    /// and basic membership checks.</para>
    /// <para>Containers are used for scene graphs, entity holders, asset caches, and similar structures.</para>
    /// </remarks>
    /// <example>
    /// Using a container:
    /// <code>
    /// IDataContainer container = ...;
    /// IIdentifiableQuantumObject obj = ...;
    /// container.ContainsKey(obj.UID); // true if stored
    /// var retrieved = container.Get(obj.UID);
    /// </code>
    /// </example>
    public interface IDataContainer
        : IRegistrableQuant
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Retrieves a stored quantum object by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the object.</param>
        /// <returns>The object if found; otherwise, <c>null</c>.</returns>
        IIdentifiableQuantumObject Get(IIdentifier id);
#else
#nullable enable
        /// <summary>
        /// Retrieves a stored quantum object by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the object.</param>
        /// <returns>The object if found; otherwise, <c>null</c>.</returns>
        IIdentifiableQuantumObject? Get(IIdentifier id);
#endif

        /// <summary>
        /// Checks whether the container has an object with the specified key.
        /// </summary>
        /// <param name="key">The identifier to check.</param>
        /// <returns><c>true</c> if an object with the given key exists; otherwise, <c>false</c>.</returns>
        bool ContainsKey(IIdentifier key);

        /// <summary>
        /// Checks whether the container holds the specified object instance.
        /// </summary>
        /// <param name="obj">The object to look for.</param>
        /// <returns><c>true</c> if the object is present; otherwise, <c>false</c>.</returns>
        bool Contains(IIdentifiableQuantumObject obj);

        /// <summary>
        /// Gets the number of objects currently stored in the container.
        /// </summary>
        /// <value>The total count of contained objects.</value>
        int Count { get; }

        /// <summary>
        /// Gets a value indicating whether the container is empty.
        /// </summary>
        /// <value><c>true</c> if <see cref="Count"/> is zero; otherwise, <c>false</c>.</value>
        bool IsEmpty { get; }
    }
}