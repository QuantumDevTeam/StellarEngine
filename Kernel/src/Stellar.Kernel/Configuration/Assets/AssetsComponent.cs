namespace Stellar.Kernel.Configuration.Assets
{
    /// <summary>
    /// A configuration component that describes asset bundles used by the engine.
    /// </summary>
    /// <remarks>
    /// Inherits from <see cref="ConfigurationComponent"/> and specifies external and embedded assets.
    /// This component is built as an <see cref="ConfigurationComponentBuildType.EngineComponent"/>.
    /// </remarks>
    public class AssetsComponent
        : ConfigurationComponent
    {
        /// <summary>
        /// Gets the build type of this component. Always <see cref="ConfigurationComponentBuildType.EngineComponent"/>.
        /// </summary>
        public override ConfigurationComponentBuildType ComponentBuildType { get; } =
            ConfigurationComponentBuildType.EngineComponent;

        /// <summary>
        /// Gets or sets an array of assets that are loaded from external files (not embedded).
        /// </summary>
        public AssetData[] ExternalAssets { get; set; }

        /// <summary>
        /// Gets or sets an array of assets that are embedded in the compiled artifact (e.g., resources).
        /// </summary>
        public AssetData[] EmbeddedAssets { get; set; }
    }
}