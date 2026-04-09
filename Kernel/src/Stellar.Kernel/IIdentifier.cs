using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel
{
    /// <summary>
    /// Represents a unique identifier used throughout the Engine to distinguish objects.
    /// </summary>
    /// <remarks>
    /// <para>This is the base identifier contract for all uniquely identifiable elements in Stellar.
    /// It inherits <see cref="IRegistrableQuantumObject"/> to allow registration in global or local registries.</para>
    /// <para>Implementations typically wrap a <see cref="Guid"/> and provide comparison, hashing, and serialization.</para>
    /// </remarks>
    /// <example>
    /// Creating and using an identifier:
    /// <code>
    /// IIdentifier id = new MyIdentifier(Guid.NewGuid());
    /// Console.WriteLine($"Object UID: {id.UID}");
    /// </code>
    /// </example>
    public interface IIdentifier
        : IRegistrableQuantumObject
    {
        /// <summary>
        /// Gets the globally unique identifier (GUID) associated with this object.
        /// </summary>
        /// <value>A <see cref="Guid"/> that never changes during the object's lifetime.</value>
        /// <remarks>
        /// The <see cref="UID"/> is the primary key for object tracking, persistence, and network replication.
        /// Two different <see cref="IIdentifier"/> instances with the same <see cref="UID"/> are considered equal.
        /// </remarks>
        Guid UID { get; }
    }
}