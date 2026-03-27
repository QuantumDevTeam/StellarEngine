using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    public interface IFailure : IQuantumObject
    {
        string Message { get; }
#if NETSTANDARD2_0
        Exception InnerException { get; }
#else
#nullable enable
        Exception? InnerException { get; }
#endif
        FailureType Type { get; }
        IFailureLevel Level { get; }
        string Source { get; }
    }
}