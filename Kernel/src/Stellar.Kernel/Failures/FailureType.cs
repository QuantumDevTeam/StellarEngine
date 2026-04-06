using System;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Type of Failures  
    /// </summary>
    /// <remarks>
    /// Mark Failure base/module/context/etc.
    /// </remarks>
    [Flags]
    public enum FailureType
    {
        /// <summary>
        /// Unknown Failure
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Engine failure
        /// </summary>
        Engine = 1 << 0,

        /// <summary>
        /// Failure gotten in a module
        /// </summary>
        Module = 1 << 1,

        /// <summary>
        /// Failure gotten in a native lib
        /// </summary>
        Native = 1 << 2,

        // Engine modules

        /// <summary>
        /// Failure gotten in Core Engine Module
        /// </summary>
        Core = 1 << 10,

        /// <summary>
        /// Failure gotten in FileSystem Engine Module
        /// </summary>
        FileSystem = 1 << 11,

        /// <summary>
        /// Failure gotten in Logging Engine Module
        /// </summary>
        Logging = 1 << 12,

        // TODO: add all other modules
        // Time = 1 << 13, // in work
        // Threading = 1 << 14, // in work

        // Context

        /// <summary>
        /// Failure gotten in a Pre action
        /// </summary>
        PreAction = 1 << 24,

        /// <summary>
        /// Failure gotten in a Post action
        /// </summary>
        PostAction = 1 << 25,

        /// <summary>
        /// Failure gotten Events action
        /// </summary>
        Events = 1 << 26,

        /// <summary>
        /// Failure gotten Update action
        /// </summary>
        Update = 1 << 27,

        /// <summary>
        /// Failure gotten Render action
        /// </summary>
        Render = 1 << 28,
        // TODO: add other context types

        // TODO: add other type enum params (optional)
    }
}