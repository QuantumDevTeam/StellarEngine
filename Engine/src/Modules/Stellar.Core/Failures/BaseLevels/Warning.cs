namespace Stellar.Core.Failures.BaseLevels;

public class Warning(IFailureDispatcherMeta dispatcherMeta)
    : FailureLevel("S.C/Warning", dispatcherMeta)
{
    public static Warning Instance = new(FailureDispatcher.DefaultMeta);

    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = true;
    public override bool IsStopExecute { get; } = false;
    public override bool IsCritical { get; } = false;
    public override bool ShouldTerminate { get; } = false;
}