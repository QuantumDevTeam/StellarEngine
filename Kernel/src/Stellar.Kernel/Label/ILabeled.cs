using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Label
{
    /// <summary>
    /// Extends <see cref="IIdentifiableQuantumObject"/> with a human‑readable label.
    /// </summary>
    /// <remarks>
    /// <para>Labels provide a named reference to a quantized object, decoupling the name from the underlying
    /// <see cref="IIdentifier"/>. The label itself is a registrable meta‑quant (<see cref="ILabel"/>),
    /// allowing lookups by name in addition to GUID.</para>
    /// <para>This is useful for scripting, debugging, and serialization where readable names are preferred
    /// over raw GUIDs.</para>
    /// </remarks>
    public interface ILabeled
        : IIdentifiableQuantumObject
    {
        /// <summary>
        /// Gets the label associated with this object.
        /// </summary>
        /// <value>An <see cref="ILabel"/> instance that provides a string name and its own identity.</value>
        /// <remarks>
        /// The label is optional – an object may return <c>null</c> if no label is assigned.
        /// Labels can be changed at runtime, but the underlying <see cref="IIdentifier.UID"/> remains constant.
        /// </remarks>
        ILabel Label { get; }
    }
}