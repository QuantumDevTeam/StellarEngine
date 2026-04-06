using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel
{
    public interface IIdentifier
        : IRegistrableQuantumObject
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        Guid UID { get; }
    }
}