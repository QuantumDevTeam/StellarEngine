using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration
{
    public abstract class ConfigurationComponent
        : IQuantumObject
    {
        public abstract ConfigurationComponentBuildType ComponentBuildType { get; }
    }
}