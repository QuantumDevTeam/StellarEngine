using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Configuration.Assets
{
    public class AssetData : IQuantumObject
    {
        public string Path { get; set; }
        public string OriginalPath { get; set; }
    }
}