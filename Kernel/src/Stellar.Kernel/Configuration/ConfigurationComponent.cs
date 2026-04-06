using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration
{
    /// <summary>
    /// Runtime configuration Component data
    /// </summary>
    public abstract class ConfigurationComponent
        : IQuantumObject
    {
        /// <summary>
        /// Type for S.R.I Builder
        /// </summary>
        public abstract ConfigurationComponentBuildType ComponentBuildType { get; }
    }
}