using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Represents a failure (exception or error) caught during engine or game operation.
    /// </summary>
    /// <remarks>
    /// <para>This interface abstracts the underlying <see cref="Exception"/> and provides additional
    /// metadata such as failure type, level, and source location.</para>
    /// <para><see cref="IFailure"/> objects are created by the failure subsystem and passed to handlers
    /// via <see cref="IFailureContextData"/>.</para>
    /// </remarks>
    public interface IFailure
        : IQuantumObject
    {
        /// <summary>
        /// Gets the failure message (typically the same as <see cref="Exception.Message"/>).
        /// </summary>
        string Message { get; }

#if NETSTANDARD2_0
        /// <summary>
        /// Gets the inner exception that caused this failure, if any.
        /// </summary>
        Exception InnerException { get; }
#else
#nullable enable
        /// <summary>
        /// Gets the inner exception that caused this failure, or <c>null</c> if none exists.
        /// </summary>
        Exception? InnerException { get; }
#endif

        /// <summary>
        /// Gets the type of the failure (origin category).
        /// </summary>
        FailureType Type { get; }

        /// <summary>
        /// Gets the severity level of the failure.
        /// </summary>
        IFailureLevel Level { get; }

        /// <summary>
        /// Gets the source location (e.g., class name, method name, or file path) where the failure occurred.
        /// </summary>
        string Source { get; }
    }
}