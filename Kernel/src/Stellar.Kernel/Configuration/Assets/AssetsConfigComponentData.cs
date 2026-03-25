using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration.Assets
{
    public class AssetsConfigComponentData : IQuantumObject
    {
        public AssetConfigInfo[] ExternalAssets { get; set; }
        public AssetConfigInfo[] EmbeddedAssets { get; set; }
    }
}