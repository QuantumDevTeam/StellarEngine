namespace Stellar.Core.Failures.BaseLevels;

public class Error() : FailureLevel("S.C/Error")
{
    public static Error Instance = new();
    
    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = true;
    public override bool IsStopExecute { get; } = true;
    public override bool IsCritical { get; } = true;
    public override bool ShouldTerminate { get; } = false;
}