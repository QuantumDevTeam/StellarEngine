using Spectre.Console;
using Spectre.Console.Cli;
using Stellar.Tools;

namespace Stellar.CLI.Commands.Version;

public sealed class OrchesterVersionCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine(StellarEnvironment.StellarOrchesterVersion);
        return 0;
    }
}