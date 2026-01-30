using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands.Version;

public sealed class SdkVersionCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine($"SDK versin {StellarVersions.SdkVersion}");
        return 0;
    }
}