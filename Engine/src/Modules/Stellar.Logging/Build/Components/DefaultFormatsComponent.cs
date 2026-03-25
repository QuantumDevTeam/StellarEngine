using Stellar.Kernel.Configuration;
using Stellar.Logging.Format;

namespace Stellar.Logging.Build.Components;

public class DefaultFormatsComponent : ConfigurationComponent
{
    public override ConfigurationComponentBuildType ComponentBuildType { get; } =
        ConfigurationComponentBuildType.Default;

    public string DefaultFileNameFormat { get; set; }
    public LoggingFormatsJson DefaultLoggingFormats { get; set; }
}