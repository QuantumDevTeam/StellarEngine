using Stellar.Tools.Configuration.Project.Assets;
using Stellar.Tools.Configuration.Project.Localizations;

namespace Stellar.Tools.Configuration.Project
{
    /// <summary>
    /// Represent .stellar.project[Project] field
    /// </summary>
    public class ProjectConfigurationObject
    {
        /// <summary>
        /// Entry point of module
        /// </summary>
        public string StellarEntryPoint { get; set; }
        
        /// <summary>
        /// assets for module
        /// </summary>
        public AssetsConfigurationObject Assets { get; set; }
        
        /// <summary>
        /// Localizations for module
        /// </summary>
        public LocalizationsConfigurationObject Localizations { get; set; }
    }
}