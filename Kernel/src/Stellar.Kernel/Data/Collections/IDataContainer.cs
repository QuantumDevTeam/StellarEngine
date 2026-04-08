using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Collections
{
    /// <summary>
    /// Quant which can contain any other QuantumObject
    /// </summary>
    public interface IDataContainer
        : IRegistrableQuant
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Get stored Quant by his Identifier 
        /// </summary>
        /// <param name="id">Object UID</param>
        /// <returns>Object</returns>
        IIdentifiableQuantumObject Get(IIdentifier id);
#else
#nullable enable
        /// <summary>
        /// Get stored Quant by his Identifier 
        /// </summary>
        /// <param name="id">Object UID</param>
        /// <returns>Object</returns>
        IIdentifiableQuantumObject? Get(IIdentifier id);
#endif
        /// <summary>
        /// Check key on existing in container
        /// </summary>
        /// <param name="key">Object UID</param>
        /// <returns>Is existing</returns>
        bool ContainsKey(IIdentifier key);
        
        /// <summary>
        /// Check object on existing in container
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>Is existing</returns>
        bool Contains(IIdentifiableQuantumObject obj);

        /// <summary>
        /// Number of contains objects
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Is container empty
        /// </summary>
        bool IsEmpty { get; }
    }
}