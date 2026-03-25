namespace Stellar.Kernel.Configuration
{
    public abstract class ConfigurationComponent : ILabeled
    {
        public abstract string Name { get; }
    }
}