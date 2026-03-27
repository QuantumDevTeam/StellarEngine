using Stellar.Kernel.Failures;

namespace Stellar.Core.Failures.BaseLevels;

public class Error : IFailureLevel
{
    public bool IsEnabled { get; set; } = true;
    public bool IsLoggable { get; } = true;
    public bool IsStopExecute { get; } = true;
    public bool IsCritical { get; } = true;
    public bool ShouldTerminate { get; } = false;
}