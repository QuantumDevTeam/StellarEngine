using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Label
{
    /// <summary>
    /// Label linked to an Identifier
    /// </summary>
    public interface ILabel
        : IRegistrableMetaQuant
    {
        /// <summary>
        /// Label name - value of Label
        /// </summary>
        string Name { get; }
    }
}