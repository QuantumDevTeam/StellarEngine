using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration.Assets
{
    /// <summary>
    /// Assets Data for Assets Config Component
    /// </summary>
    public class AssetData
        : IQuantumObject
    {
        /// <summary>
        /// Asset path
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// original path in project
        /// </summary>
        public string OriginalPath { get; set; }
    }
}