namespace Stellar.Core.Failures.BaseLevels;

public class Error(IFailureDispatcherMeta dispatcherMeta) 
    : FailureLevel("S.C/Error", dispatcherMeta)
{
    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = true;
    public override bool IsStopExecute { get; } = true;
    public override bool IsCritical { get; } = true;
    public override bool ShouldTerminate { get; } = false;
}