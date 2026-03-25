namespace Stellar.Kernel.Configuration.Assets
{
    public class AssetsComponent : ConfigurationComponent
    {
        public override ConfigurationComponentBuildType ComponentBuildType { get; } =
            ConfigurationComponentBuildType.EngineComponent;

        public AssetData[] ExternalAssets { get; set; }
        public AssetData[] EmbeddedAssets { get; set; }
    }
}