using Stellar.Kernel.Configuration;
using Stellar.LoggingSystem.Format;

namespace Stellar.LoggingSystem.Build.Components;

// TODO: Implement config generations
public class DefaultFormatsComponent : ConfigurationComponent
{
    public override ConfigurationComponentBuildType ComponentBuildType { get; } =
        ConfigurationComponentBuildType.Default;

    public string DefaultFileNameFormat { get; set; }
    public LoggingFormatsJson DefaultLoggingFormats { get; set; }
}