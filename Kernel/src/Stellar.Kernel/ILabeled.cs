using Stellar.Kernel.Quantization;

namespace Stellar.Kernel
{
    public interface ILabeled : IQuantumObject
    {
        string Name { get; }
    }
}