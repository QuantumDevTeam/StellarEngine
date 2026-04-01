using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Label
{
    public interface ILabel
        : IRegistrableMetaQuant
    {
        string Name { get; }
    }
}