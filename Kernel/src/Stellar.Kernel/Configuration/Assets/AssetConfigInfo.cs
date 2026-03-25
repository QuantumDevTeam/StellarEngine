using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration.Assets
{
    public class AssetConfigInfo : IQuantumObject
    {
        public string Path { get; set; }
        public string OriginalPath { get; set; }
    }
}