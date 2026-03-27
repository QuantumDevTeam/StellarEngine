using System;

namespace Stellar.Kernel.Failures
{
    [Flags]
    public enum FailureType
    {
        Unknown = 0,
        Engine = 1 << 0,
        Module = 1 << 1,
        Native = 1 << 2,

        // Engine modules
        Core = 1 << 10,
        FileSystem = 1 << 11,
        Logging = 1 << 12,

        // TODO: add all other modules
        Time = 1 << 13, // in work
        Threading = 1 << 14, // in work

        // Context
        PreAction = 1 << 24,
        PostAction = 1 << 25,
        Events = 1 << 26,
        Update = 1 << 27,
        Render = 1 << 28,
        // TODO: add other context types
        
        // TODO: add other type enum params (optional)
    }
}