using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel
{
    public interface IIdentifier : IQuantumObject
    {
        Guid UID { get; }
    }
}