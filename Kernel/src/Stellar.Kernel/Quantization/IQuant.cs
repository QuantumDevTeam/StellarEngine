namespace Stellar.Kernel.Quantization
{
    public interface IQuant
    {
        IMetaQuant Meta { get; }
        IIdentifier Id { get; }
    }
}