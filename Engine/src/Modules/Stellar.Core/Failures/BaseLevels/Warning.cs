namespace Stellar.Core.Failures.BaseLevels;

public class Warning() : FailureLevel("S.C/Warning")
{
    public static Warning Instance = new();
    
    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = true;
    public override bool IsStopExecute { get; } = false;
    public override bool IsCritical { get; } = false;
    public override bool ShouldTerminate { get; } = false;
}