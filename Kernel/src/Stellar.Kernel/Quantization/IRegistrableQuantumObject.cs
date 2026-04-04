namespace Stellar.Kernel.Quantization
{
    public interface IRegistrableQuantumObject
        : IQuantumObject
    {
        void Register(IQuantumObject registry);
        void Unregister(IQuantumObject registry);
    }
}