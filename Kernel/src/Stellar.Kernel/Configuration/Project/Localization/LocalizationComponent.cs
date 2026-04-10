namespace Stellar.Kernel.Configuration.Project.Localization
{
    /// <summary>
    /// Localization runtime data
    /// </summary>
    public class LocalizationComponent
        : ConfigurationComponent
    {
        /// <summary>
        /// Localizations - field of Project object in .stellar.project file
        /// </summary>
        public override ConfigurationComponentBuildType ComponentBuildType { get; } =
            ConfigurationComponentBuildType.EngineComponent;
        
        /// <summary>
        /// Default culture
        /// </summary>
        public string DefaultCulture { get; set; }
        
        /// <summary>
        /// Array with supported cultures
        /// </summary>
        public string[] SupportedCultures { get; set; }
        
        /// <summary>
        /// Array of Localization Index Files
        /// </summary>
        public LocalizationIndexData[] LocalizationIndexFiles { get; set; }
    }
}