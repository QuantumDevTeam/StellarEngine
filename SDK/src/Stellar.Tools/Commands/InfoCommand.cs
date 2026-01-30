using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands;

public sealed class InfoCommand : Command
{
    public override int Execute(CommandContext context)
    {
        var table = new Table()
            .AddColumn("Key")
            .AddColumn("Value");

        table.AddRow("Dotnet feature band", StellarEnvironment.GetDotnetFeatureBand());
        table.AddRow("Engine shared path", StellarEnvironment.GetStellarEngineSharedPath());
        table.AddRow("Engine installation path", StellarEnvironment.GetStellarEngineInstallationPath());

        AnsiConsole.Write(table);
        return 0;
    }
}