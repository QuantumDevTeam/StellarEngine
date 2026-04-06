namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// MetaQuant which can be registered in a registry
    /// </summary>
    public interface IRegistrableMetaQuant
        : IMetaQuant, IRegistrableQuantumObject
    {
    }
}