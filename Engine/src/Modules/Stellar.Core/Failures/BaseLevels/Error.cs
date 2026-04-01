using Stellar.Kernel.Label;

namespace Stellar.Core.Failures.BaseLevels;

public class Error : FailureLevel
{
    public override ILabel Label => new Label.Label(new Identifier(), "S.C/Error");
    public override bool IsEnabled { get; set; } = true;
    public override bool IsLoggable { get; } = true;
    public override bool IsStopExecute { get; } = true;
    public override bool IsCritical { get; } = true;
    public override bool ShouldTerminate { get; } = false;
}