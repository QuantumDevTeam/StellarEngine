namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// The fundamental building block of the Stellar Engine – a "Quant" (quantized object).
    /// </summary>
    /// <remarks>
    /// <para>All runtime entities, from game objects to systems, are <see cref="IQuant"/> instances.
    /// Each quant has a unique identity (<see cref="IIdentifiableQuantumObject"/>) and associated metadata
    /// (<see cref="IMetaQuant"/>).</para>
    /// <para>Quants can be registered, serialized, and networked. The separation of <see cref="IQuant"/>
    /// and <see cref="IMetaQuant"/> allows lightweight transfer of the main object while keeping metadata
    /// accessible.</para>
    /// </remarks>
    public interface IQuant
        : IIdentifiableQuantumObject
    {
        /// <summary>
        /// Gets the metadata object associated with this quant.
        /// </summary>
        /// <value>An <see cref="IMetaQuant"/> instance that describes this quant.</value>
        /// <remarks>
        /// The meta object is typically created together with the quant and remains linked for its lifetime.
        /// It can be used to store type information, custom attributes, or other non‑core data.
        /// </remarks>
        IMetaQuant Meta { get; }
    }
}