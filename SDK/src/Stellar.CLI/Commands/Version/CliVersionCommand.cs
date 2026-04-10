using Spectre.Console;
using Spectre.Console.Cli;
using Stellar.Tools;

namespace Stellar.CLI.Commands.Version;

public sealed class CliVersionCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine(StellarEnvironment.GetStellarCliVersion());
        return 0;
    }
}