using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands;

public sealed class InfoCommand : Command
{
    public override int Execute(CommandContext context)
    {
        var env = new StellarEnvironment();

        var table = new Table()
            .AddColumn("Key")
            .AddColumn("Value");

        table.AddRow("Dotnet feature band", env.GetDotnetFeatureBand());
        table.AddRow("SDK path", env.GetSdkPackagePath());
        table.AddRow("StellarEngine path", env.GetEnginePath() ?? "<not set>");
        table.AddRow("STELLAR_PATH", env.GetStellarPath() ?? "<not set>");

        AnsiConsole.Write(table);
        return 0;
    }
}