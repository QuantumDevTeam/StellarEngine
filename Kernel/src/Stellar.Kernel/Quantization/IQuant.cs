namespace Stellar.Kernel.Quantization
{
    public interface IQuant : IMetaQuant
    {
        IMetaQuant Meta { get; }
    }
}