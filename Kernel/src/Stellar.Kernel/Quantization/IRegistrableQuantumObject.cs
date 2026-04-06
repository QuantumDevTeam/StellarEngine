namespace Stellar.Kernel.Quantization
{
    /// <summary>
    /// QuantumObject which can be registered in a registry
    /// </summary>
    public interface IRegistrableQuantumObject
        : IQuantumObject
    {
        /// <summary>
        /// Registration in registry
        /// </summary>
        void Register(IQuantumObject registry);
        
        /// <summary>
        /// Unregistration from registry
        /// </summary>
        void Unregister(IQuantumObject registry);
    }
}