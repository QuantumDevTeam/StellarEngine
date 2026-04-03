namespace Stellar.Kernel.Quantization
{
    public interface IQuant : IIdentifiableQuantumObject
    {
        IMetaQuant Meta { get; }
    }
}