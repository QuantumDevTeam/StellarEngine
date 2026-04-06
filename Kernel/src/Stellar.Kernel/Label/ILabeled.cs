using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Label
{
    /// <summary>
    /// QuantumObject which has Label linked to his UID
    /// </summary>
    public interface ILabeled
        : IIdentifiableQuantumObject
    {
        /// <summary>
        /// QuantumObject Label
        /// </summary>
        ILabel Label { get; }
    }
}