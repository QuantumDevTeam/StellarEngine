using Spectre.Console;
using Spectre.Console.Cli;
using Stellar.Tools;

namespace Stellar.CLI.Commands.Version;

public sealed class SdkVersionCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine(StellarEnvironment.GetStellarSdkVersion());
        return 0;
    }
}