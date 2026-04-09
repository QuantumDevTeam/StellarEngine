namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// Represents metadata associated with a <see cref="IQuant"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>Every <see cref="IQuant"/> (a quantized object) has a corresponding <see cref="IMetaQuant"/>
    /// that holds descriptive or auxiliary data such as type information, creation time, or serialization hints.</para>
    /// <para>Meta objects themselves are identifiable (<see cref="IIdentifiableQuantumObject"/>), allowing
    /// them to be stored and referenced separately from the main quant.</para>
    /// </remarks>
    /// <example>
    /// Accessing meta data:
    /// <code>
    /// IQuant quant = ...;
    /// IMetaQuant meta = quant.Meta;
    /// Console.WriteLine($"Meta UID: {meta.UID.UID}");
    /// </code>
    /// </example>
    public interface IMetaQuant
        : IIdentifiableQuantumObject
    {
    }
}