#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Spectre.Console;
using Spectre.Console.Cli;
using XmlDocMarkdown.Core;

namespace Stellar.Tools.Commands.Project;

public class GenerateProjectCommandData : CommandSettings
{
    [CommandArgument(0, "<INPUT>")] public string Input { get; set; }

    [CommandArgument(1, "<OUTPUT>")] public string Output { get; set; }
}

public sealed class GenerateProjectDocsCommand : Command<GenerateProjectCommandData>
{
    public override int Execute(CommandContext context, GenerateProjectCommandData data)
    {
        AnsiConsole.MarkupLine(
            $"[green]Generating docs for DLL:[/]  [grey]{data.Input}[/]\n" +
            $"[green]Output directory:[/] [grey]{data.Output}[/]"
        );

        IReadOnlyList<string> args = new[] { data.Input, data.Output }
            .Concat(context.Arguments.Skip(4))
            .ToArray();

        AnsiConsole.Markup("[grey]command: xmldocmd[/]");
        foreach (var argument in args)
        {
            AnsiConsole.Markup($"[grey] {argument}[/]");
        }

        AnsiConsole.WriteLine();

        return XmlDocMarkdownApp.Run(args);
    }
}