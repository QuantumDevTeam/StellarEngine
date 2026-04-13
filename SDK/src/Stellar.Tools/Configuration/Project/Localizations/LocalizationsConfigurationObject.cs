namespace Stellar.Tools.Configuration.Project.Localizations
{
    /// <summary>
    /// Represent .stellar.project[Project][Localization] field
    /// </summary>
    public class LocalizationsConfigurationObject
    {
        /// <summary>
        /// Default culture to use
        /// </summary>
        public string DefaultCulture { get; set; }
        
        /// <summary>
        /// Supported cultures
        /// </summary>
        public string[] Cultures { get; set; }
        
        /// <summary>
        /// index files for localizations
        /// </summary>
        public string[] IndexFiles { get; set; }
    }
}