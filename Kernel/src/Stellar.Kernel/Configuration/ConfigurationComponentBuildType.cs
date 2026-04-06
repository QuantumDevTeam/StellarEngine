namespace Stellar.Kernel.Configuration
{
    /// <summary>
    /// Type of Runtime config components for S.R.I builder
    /// </summary>
    public enum ConfigurationComponentBuildType
    {
        /// <summary>
        /// Component of Engine data
        /// </summary>
        EngineComponent,

        /// <summary>
        /// Default JSON copy
        /// </summary>
        Default,

        /// <summary>
        /// Use custom task for building this Component data
        /// </summary>
        Task,

        /// <summary>
        /// Use custom script for building this Component data
        /// </summary>
        Script
    }
}