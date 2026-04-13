namespace Stellar.Tools.Configuration.Project.Assets
{
    /// <summary>
    /// Represent .stellar.project[Project][Localization] field
    /// </summary>
    public class AssetsConfigurationObject
    {
        /// <summary>
        /// Included assets
        /// </summary>
        public string[] Include { get; set; }
        
        /// <summary>
        /// Excluded assets
        /// </summary>
        public string[] Exclude { get; set; }
        
        /// <summary>
        /// Assets embedded to DLL file
        /// </summary>
        public string[] Embedded { get; set; }
    }
}