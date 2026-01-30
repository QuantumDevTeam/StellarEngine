using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands.Version;

public sealed class ToolsVersionCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine($"Tools versin {StellarVersions.ToolsVersion}");
        return 0;
    }
}