namespace Stellar.Kernel.Configuration.Assets
{
    public class AssetsComponent : ConfigurationComponent
    {
        public override string Name => "Assets";
        public AssetData[] ExternalAssets { get; set; }
        public AssetData[] EmbeddedAssets { get; set; }
    }
}