using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel
{
    /// <summary>
    /// Base Engine unique Identifier
    /// </summary>
    /// <remarks>used in all operations with engine</remarks>
    public interface IIdentifier
        : IRegistrableQuantumObject
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        Guid UID { get; }
    }
}