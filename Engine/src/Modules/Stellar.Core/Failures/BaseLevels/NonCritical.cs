namespace Stellar.Core.Failures.BaseLevels;

public class NonCritical() : FailureLevel("S.C/NonCritical")
{
    public static NonCritical Instance = new();
    
    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = false;
    public override bool IsStopExecute { get; } = false;
    public override bool IsCritical { get; } = false;
    public override bool ShouldTerminate { get; } = false;
}