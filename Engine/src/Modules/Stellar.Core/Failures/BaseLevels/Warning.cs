using Stellar.Kernel.Label;

namespace Stellar.Core.Failures.BaseLevels;

public class Warning : FailureLevel
{
    public override ILabel Label => new Label.Label(new Identifier(), "S.C/Warning");
    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = true;
    public override bool IsStopExecute { get; } = false;
    public override bool IsCritical { get; } = false;
    public override bool ShouldTerminate { get; } = false;
}