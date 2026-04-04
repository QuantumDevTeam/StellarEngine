using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel
{
    public interface IIdentifier
        : IRegistrableQuantumObject
    {
        Guid UID { get; }
    }
}