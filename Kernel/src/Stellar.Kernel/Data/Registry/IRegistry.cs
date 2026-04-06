using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Registry
{
    /// <summary>
    /// Registry for QuantumObjects
    /// </summary>
    /// <typeparam name="T">QuantumObjects Type</typeparam>
    public interface IRegistry<T>
        : IQuantumObject
        where T : IRegistrableQuantumObject
    {
        /// <summary>
        /// Check for existing in registry
        /// </summary>
        /// <param name="id">Object UID</param>
        /// <returns>Is existed</returns>
        bool Exists(IIdentifier id);

        /// <summary>
        /// Register QuantumObject
        /// </summary>
        /// <param name="obj">Object himself</param>
        /// <returns>Is registered</returns>
        bool Register(T obj);
#if NETSTANDARD2_0
        /// <summary>
        /// Get registered object
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Object</returns>
        T Get(IIdentifier id);

        /// <summary>
        /// Pop registered object
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Object</returns>
        T Pop(IIdentifier id);
#else
#nullable enable
        /// <summary>
        /// Get registered object by his UID
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Object</returns>
        T? Get(IIdentifier id);

        /// <summary>
        /// Pop registered object by his UID
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Object</returns>
        T? Pop(IIdentifier id);
#endif
        /// <summary>
        /// Number of registered objects
        /// </summary>
        int Size { get; }

        /// <summary>
        /// keys of registered objects
        /// </summary>
        System.Collections.Generic.ICollection<IIdentifier> Keys { get; }
        
        /// <summary>
        /// All registered objects
        /// </summary>
        System.Collections.Generic.ICollection<T> Values { get; }
    }
}