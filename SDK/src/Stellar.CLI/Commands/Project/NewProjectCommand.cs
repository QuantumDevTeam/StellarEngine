using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.CLI.Commands.Project;

public sealed class NewProjectCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine(
            "[red]You must specify a template:[/] stellar project <template> new <Name>"
        );
        return -1;
    }
}