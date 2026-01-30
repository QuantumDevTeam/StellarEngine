using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands.Project;

public sealed class ListTemplatesCommand : Command
{
    public override int Execute(CommandContext context)
    {
        var enginePath = StellarEnvironment.GetStellarEngineInstallationPath();

        var templatesDir = Path.Combine(
            enginePath,
            "Data",
            "Templates",
            "Projects"
        );

        if (!Directory.Exists(templatesDir))
        {
            AnsiConsole.MarkupLine("[red]No templates directory found[/]");
            return -1;
        }

        var dirs = Directory.GetDirectories(templatesDir);

        if (dirs.Length == 0)
        {
            AnsiConsole.MarkupLine("[grey]No templates available[/]");
            return 0;
        }

        foreach (var dir in dirs)
        {
            AnsiConsole.MarkupLine($"- {Path.GetFileName(dir)}");
        }

        return 0;
    }
}