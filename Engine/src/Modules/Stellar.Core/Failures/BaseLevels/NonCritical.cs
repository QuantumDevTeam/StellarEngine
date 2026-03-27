using Stellar.Kernel.Failures;

namespace Stellar.Core.Failures.BaseLevels;

public class NonCritical : IFailureLevel
{
    public bool IsEnabled { get; set; } = true;
    public bool IsLoggable { get; } = false;
    public bool IsStopExecute { get; } = false;
    public bool IsCritical { get; } = false;
    public bool ShouldTerminate { get; } = false;
}