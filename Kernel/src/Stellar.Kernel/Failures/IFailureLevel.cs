using System;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Failure Level. Indicate Failure behavior
    /// </summary>
    public interface IFailureLevel
        : IRegistrableQuantumObject, ILabeled, IDisposable
    {
        /// <summary>
        /// Level is enabled
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Should be logged in handling operation
        /// </summary>
        bool IsLoggable { get; }

        /// <summary>
        /// Should stop current execution context after handling
        /// </summary>
        bool IsStopExecute { get; }

        /// <summary>
        /// Criticality of Failure
        /// </summary>
        bool IsCritical { get; }

        /// <summary>
        /// Failure Level should terminate all game and engine execution
        /// </summary>
        bool ShouldTerminate { get; }
    }
}