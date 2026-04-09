using System;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Flags enumeration that categorizes the origin or context of a failure.
    /// </summary>
    /// <remarks>
    /// <para>Failure types are used to route failures to appropriate handlers and to provide
    /// detailed information about where the failure occurred (engine, module, native code, etc.).</para>
    /// <para>The flags can be combined using bitwise OR operations to represent multiple categories.</para>
    /// </remarks>
    [Flags]
    public enum FailureType
    {
        /// <summary>
        /// The failure type cannot be determined.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A failure originating from the engine itself.
        /// </summary>
        Engine = 1 << 0,

        /// <summary>
        /// A failure originating from an Engine/Game/Custom module.
        /// </summary>
        Module = 1 << 1,

        /// <summary>
        /// A failure originating from a native (unmanaged) library.
        /// </summary>
        Native = 1 << 2,

        // Engine modules

        /// <summary>
        /// A failure originating from the Core engine module.
        /// </summary>
        Core = 1 << 10,

        /// <summary>
        /// A failure originating from the FileSystem engine module.
        /// </summary>
        FileSystem = 1 << 11,

        /// <summary>
        /// A failure originating from the Logging engine module.
        /// </summary>
        Logging = 1 << 12,

        // TODO: add all other modules
        // Time = 1 << 13,
        // Threading = 1 << 14,

        // Context (execution phases)

        /// <summary>
        /// A failure that occurred during a pre‑action phase (e.g., before main processing).
        /// </summary>
        PreAction = 1 << 24,

        /// <summary>
        /// A failure that occurred during a post‑action phase (e.g., cleanup after processing).
        /// </summary>
        PostAction = 1 << 25,

        /// <summary>
        /// A failure that occurred during event dispatching or handling.
        /// </summary>
        Events = 1 << 26,

        /// <summary>
        /// A failure that occurred during the update phase of a game or module loop.
        /// </summary>
        Update = 1 << 27,

        /// <summary>
        /// A failure that occurred during the rendering phase.
        /// </summary>
        Render = 1 << 28,
        // TODO: add other context types
    }
}