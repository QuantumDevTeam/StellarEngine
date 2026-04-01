using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Label
{
    public interface ILabeled
        : IQuantumObject
    {
        ILabel Label { get; }
    }
}