using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Defines the severity and behavior of a failure.
    /// </summary>
    /// <remarks>
    /// <para>Each failure has a level that determines whether it should be logged, whether it stops execution,
    /// whether it is critical, and whether it should terminate the entire engine.</para>
    /// <para>Levels are registrable, labeled, and disposable objects, allowing runtime modification and disposal.</para>
    /// </remarks>
    public interface IFailureLevel
        : IRegistrableQuantumObject, ILabeled
    {
        /// <summary>
        /// Gets or sets whether this failure level is enabled. Disabled levels are ignored.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets whether failures of this level should be logged automatically.
        /// </summary>
        bool IsLoggable { get; }

        /// <summary>
        /// Gets whether failures of this level should stop the current execution context (e.g., a task or a frame).
        /// </summary>
        bool IsStopExecute { get; }

        /// <summary>
        /// Gets whether the failure is considered critical (may require special handling).
        /// </summary>
        bool IsCritical { get; }

        /// <summary>
        /// Gets whether the failure should terminate the entire engine and game process.
        /// </summary>
        bool ShouldTerminate { get; }
    }
}