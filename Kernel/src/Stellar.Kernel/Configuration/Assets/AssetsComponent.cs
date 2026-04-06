namespace Stellar.Kernel.Configuration.Assets
{
    /// <summary>
    /// Assets Component for Runtime configuration
    /// </summary>
    public class AssetsComponent
        : ConfigurationComponent
    {
        /// <summary>
        /// Build Type
        /// </summary>
        public override ConfigurationComponentBuildType ComponentBuildType { get; } =
            ConfigurationComponentBuildType.EngineComponent;

        /// <summary>
        /// External assets
        /// </summary>
        public AssetData[] ExternalAssets { get; set; }
        
        /// <summary>
        /// Assets embedded in artifact
        /// </summary>
        public AssetData[] EmbeddedAssets { get; set; }
    }
}