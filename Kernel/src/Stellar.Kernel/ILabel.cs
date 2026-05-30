using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Label
{
    /// <summary>
    /// Represents a named label linked to an <see cref="IIdentifier"/>.
    /// </summary>
    /// <remarks>
    /// <para>A label is a registrable meta‑quant (<see cref="IRegistrableMetaQuant"/>), meaning it can be stored
    /// in a global name‑to‑identifier registry. The label’s own <see cref="IIdentifier"/> allows it to be
    /// tracked, while its <see cref="Name"/> provides a human‑readable alias.</para>
    /// <para>Labels are often used for named resources (e.g., "Player", "MainCamera") or for serialization
    /// where human readability is important.</para>
    /// </remarks>
    /// <example>
    /// Creating and using a label:
    /// <code>
    /// ILabel label = new MyLabel(Guid.NewGuid(), "Hero");
    /// Console.WriteLine($"Label: {label.Name}, UID: {label.UID.UID}");
    /// </code>
    /// </example>
    public interface ILabel
        : IRegistrableMetaQuant
    {
        /// <summary>
        /// Gets the human‑readable name of the label.
        /// </summary>
        /// <value>The label string. It is not guaranteed to be unique globally.</value>
        /// <remarks>
        /// The name can be changed at runtime. Implementations should ensure that renaming updates
        /// any relevant registries to keep name‑to‑identifier mappings consistent.
        /// </remarks>
        string Name { get; }
    }
}