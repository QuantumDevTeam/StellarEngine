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

        table.AddRow("Orchester version", StellarEnvironment.StellarOrchesterVersion);
        table.AddRow("Engine shared path", StellarEnvironment.StellarOrchesterSharedDir);
        table.AddRow("Engine installation path", StellarEnvironment.StellarOrchesterInstallationDir);

        AnsiConsole.Write(table);
        return 0;
    }
}