namespace Stellar.Kernel.Quantization
{
    public interface IRegistrableQuantumObject : IQuantumObject
    {
        void Register();
        void Unregister();
    }
}