using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands.Version;

public sealed class KernelVersionCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine(StellarEnvironment.GetStellarKernelVersion());
        return 0;
    }
}