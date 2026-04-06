namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// QuantumObject which has UID
    /// </summary>
    public interface IIdentifiableQuantumObject
        : IQuantumObject
    {
        /// <summary>
        /// Unique ID as Identifier
        /// </summary>
        IIdentifier UID { get; }
    }
}