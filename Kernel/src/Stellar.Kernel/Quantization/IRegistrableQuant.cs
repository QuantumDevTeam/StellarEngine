namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// Quant which can be registered in a registry
    /// </summary>
    public interface IRegistrableQuant
        : IQuant, IRegistrableQuantumObject
    {
    }
}