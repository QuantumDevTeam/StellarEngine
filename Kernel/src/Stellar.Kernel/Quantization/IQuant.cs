namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// Base engine Type - Quant
    /// </summary>
    public interface IQuant
        : IIdentifiableQuantumObject
    {
        /// <summary>
        /// Quant's Meta
        /// </summary>
        /// <remarks>
        /// As base MetaQuant
        /// </remarks>
        IMetaQuant Meta { get; }
    }
}