using System;

namespace Stellar.Kernel.Configuration
{
    /// <summary>
    /// Specifies the method used to build a <see cref="ConfigurationComponent"/>.
    /// </summary>
    public enum ConfigurationComponentBuildType
    {
        /// <summary>
        /// The component is built by the engine itself (e.g., assets, threading).
        /// </summary>
        EngineComponent,

        /// <summary>
        /// The component is built by copying a default JSON file.
        /// </summary>
        Default,

        /// <summary>
        /// A custom build task (MSBuild or similar) is responsible for constructing this component.
        /// </summary>
        [Obsolete("Task based generation not implemented")]
        Task,

        /// <summary>
        /// A custom script (e.g., PowerShell, Python) is responsible for constructing this component.
        /// </summary>
        [Obsolete("Script based generation not implemented")]
        Script
    }
}