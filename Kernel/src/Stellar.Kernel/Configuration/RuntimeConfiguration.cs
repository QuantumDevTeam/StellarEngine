namespace Stellar.Kernel.Configuration
{
    public abstract class RuntimeConfiguration
    {
        public string ProjectName { get; set; }
        public string CompanyName { get; set; }
        public string Version { get; set; }
        
        public string StellarOrchesterVersion { get; set; }
        public string StellarEngineVersion { get; set; }
        
        public string EntryPoint { get; set; }
        public string BuildDate { get; set; }
        
        public AssetsConfigComponentData Assets { get; set; }
    }
}