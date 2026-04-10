using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.CLI.Commands.Project;

public sealed class ProjectInfoCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine("[yellow]Project info command (MVP)[/]");
        return 0;
    }
}