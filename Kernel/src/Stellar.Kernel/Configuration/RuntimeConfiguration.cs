using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration
{
    /// <summary>
    /// Runtime config for EntryPoint
    /// </summary>
    public abstract class RuntimeConfiguration
        : IQuantumObject
    {
        /// <summary>
        /// Name of current CS project
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Company name which create this project
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Project version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Version of Stellar Orchester
        /// </summary>
        public string StellarOrchesterVersion { get; set; }

        /// <summary>
        /// Version of Stellar Engine
        /// </summary>
        public string StellarEngineVersion { get; set; }

        /// <summary>
        /// Program Entry Point 
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        /// Program Build date
        /// </summary>
        public string BuildDate { get; set; }

        /// <summary>
        /// Runtime config components
        /// </summary>
        public ConfigurationComponent[] Components { get; set; }
    }
}