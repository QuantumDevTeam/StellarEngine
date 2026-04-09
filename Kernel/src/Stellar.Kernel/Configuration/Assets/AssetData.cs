using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration.Assets
{
    /// <summary>
    /// Represents an asset entry in the runtime configuration.
    /// </summary>
    /// <remarks>
    /// Contains the runtime path and the original project path of an asset.
    /// This class is a simple data container (not an interface) and implements <see cref="IQuantumObject"/>.
    /// </remarks>
    public class AssetData
        : IQuantumObject
    {
        /// <summary>
        /// Gets or sets the runtime filesystem path of the asset.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the original path of the asset inside the source project.
        /// </summary>
        public string OriginalPath { get; set; }
    }
}