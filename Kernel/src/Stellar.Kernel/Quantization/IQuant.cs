namespace Stellar.Kernel.Quantization
{
    public interface IQuant : IQuantumObject
    {
        IIdentifier UID { get; }
        IMetaQuant Meta { get; }
    }
}