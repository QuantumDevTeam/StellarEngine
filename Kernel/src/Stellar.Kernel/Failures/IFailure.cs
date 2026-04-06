using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Failure caught in engine or game operations
    /// </summary>
    public interface IFailure
        : IQuantumObject
    {
        /// <summary>
        /// Failure message, equals Exception.Message
        /// </summary>
        string Message { get; }
#if NETSTANDARD2_0
        /// <summary>
        /// Inner exception, equals Exception.InnerException
        /// </summary>
        Exception InnerException { get; }
#else
#nullable enable
        /// <summary>
        /// Inner exception, equals Exception.InnerException
        /// </summary>
        Exception? InnerException { get; }
#endif

        /// <summary>
        /// Failure Type
        /// </summary>
        FailureType Type { get; }

        /// <summary>
        /// Failure Level
        /// </summary>
        IFailureLevel Level { get; }

        /// <summary>
        /// Location that caused Exception
        /// </summary>
        string Source { get; }
    }
}