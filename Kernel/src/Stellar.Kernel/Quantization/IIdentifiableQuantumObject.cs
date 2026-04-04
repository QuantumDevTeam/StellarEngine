namespace Stellar.Kernel.Quantization
{
    public interface IIdentifiableQuantumObject
        : IQuantumObject
    {
        IIdentifier UID { get; }
    }
}