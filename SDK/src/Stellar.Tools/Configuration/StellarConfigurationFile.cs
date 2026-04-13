using Stellar.Tools.Configuration.Project;

namespace Stellar.Tools.Configuration
{
    /// <summary>
    /// Represent data for .stellar.project file
    /// </summary>
    public class StellarConfigurationFile
    {
        /// <summary>
        /// Represent .stellar.project[Project] field
        /// </summary>
        public ProjectConfigurationObject ProjectConfig { get; set; }

        // /// <summary>
        // /// Represent .stellar.project[Runtime] field
        // /// </summary>
        // public Dictionary<string, ...> RuntimeConfig { get; set; } // TODO: RuntimeConfig export from parser
    }
}