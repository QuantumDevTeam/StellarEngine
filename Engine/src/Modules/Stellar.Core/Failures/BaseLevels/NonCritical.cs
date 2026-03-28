namespace Stellar.Core.Failures.BaseLevels;

public class NonCritical : FailureLevel
{
    public override string Name => "S.C/NonCritical";
    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = false;
    public override bool IsStopExecute { get; } = false;
    public override bool IsCritical { get; } = false;
    public override bool ShouldTerminate { get; } = false;
}