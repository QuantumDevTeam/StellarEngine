using Stellar.Kernel.Configuration.Project.Assets;

namespace Stellar.Kernel.Configuration.Project.Localization
{
    /// <summary>
    /// Information about Localization Index File.
    /// </summary>
    /// <remarks>Inherits AssetData and have Path parameters</remarks>
    public class LocalizationIndexData
        : AssetData
    {
        /// <summary>
        /// Name of Culture
        /// </summary>
        public string Culture { get; set; }
    }
}