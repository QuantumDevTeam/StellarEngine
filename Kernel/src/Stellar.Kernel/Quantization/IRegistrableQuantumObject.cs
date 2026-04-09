using System;

namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// Marks a <see cref="IQuantumObject"/> that can be added to or removed from a registry.
    /// </summary>
    /// <remarks>
    /// <para>Registries (implementing <see cref="IQuantumObject"/>) act as containers for objects that need
    /// global or scoped access. Examples: entity registry, task registry, resource registry.</para>
    /// <para>Implementing this interface allows the object to notify the registry of its creation or destruction,
    /// enabling automatic cleanup and lookup.</para>
    /// </remarks>
    /// <example>
    /// Registration pattern:
    /// <code>
    /// public class MyRegistry : IQuantumObject { ... }
    /// 
    /// IRegistrableQuantumObject obj = ...;
    /// IQuantumObject registry = new MyRegistry();
    /// obj.Register(registry);
    /// // ... use obj
    /// obj.Unregister(registry);
    /// </code>
    /// </example>
    public interface IRegistrableQuantumObject
        : IQuantumObject, IDisposable
    {
        /// <summary>
        /// Registers this object with the specified registry.
        /// </summary>
        /// <param name="registry">The registry container that will store a reference to this object.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="registry"/> is <c>null</c>.</exception>
        /// <remarks>
        /// The exact registration semantics (e.g., key type, duplicate handling) are defined by the registry implementation.
        /// Typically, the registry uses the object's <see cref="IIdentifiableQuantumObject.UID"/> as the key.
        /// </remarks>
        void Register(IQuantumObject registry);
        
        /// <summary>
        /// Unregisters this object from the specified registry.
        /// </summary>
        /// <param name="registry">The registry from which to remove this object.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="registry"/> is <c>null</c>.</exception>
        /// <remarks>
        /// After unregistration, the registry should no longer return this object in lookups.
        /// Calling <see cref="Unregister"/> on an object that was not previously registered should have no effect.
        /// </remarks>
        void Unregister(IQuantumObject registry);
    }
}