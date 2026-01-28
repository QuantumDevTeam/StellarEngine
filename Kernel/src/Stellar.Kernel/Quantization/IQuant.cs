using Stellar.Kernel.Identification;

namespace Stellar.Kernel.Quantization
{
    public interface IQuant
    {
        IQuantMeta Meta { get; }
        IIdentifier Id { get; }
    }
}