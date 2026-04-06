using Spectre.Console.Cli;
using Stellar.Tools.Commands;
using Stellar.Tools.Commands.Project;
using Stellar.Tools.Commands.Sdk;
using Stellar.Tools.Commands.Version;

var app = new CommandApp();

app.Configure(config =>
{
    config.SetApplicationName("stellar");

    config.AddCommand<InfoCommand>("info")
        .WithDescription("Show StellarEngine information");

    config.AddBranch("version", sdk =>
    {
        sdk.SetDescription("Systems versions");

        sdk.AddCommand<OrchesterVersionCommand>("orchester")
            .WithDescription("Tools Version");

        sdk.AddCommand<ToolsVersionCommand>("tools")
            .WithDescription("Tools Version");

        sdk.AddCommand<SdkVersionCommand>("sdk")
            .WithDescription("SDK Version");

        sdk.AddCommand<KernelVersionCommand>("kernel")
            .WithDescription("Kernel Version");

        sdk.AddCommand<EngineVersionCommand>("engine")
            .WithDescription("Engine Version");
    });

    config.AddBranch("sdk", sdk =>
    {
        sdk.SetDescription("SDK related commands");

        sdk.AddCommand<InstallSdkCommand>("install")
            .WithDescription("Install Stellar SDK workload");
    });

    config.AddBranch("project", project =>
    {
        project.SetDescription("Project related commands");

        project.AddCommand<ProjectInfoCommand>("info")
            .WithDescription("Show project information");

        project.AddCommand<NewProjectCommand>("new")
            .WithAlias("n")
            .WithDescription("Create new project from template");

        project.AddCommand<GenerateProjectDocsCommand>("generate-docs")
            .WithAlias("gendoc")
            .WithData(typeof(GenerateProjectCommandData))
            .WithDescription("Show project information");

        project.AddBranch("template", template =>
        {
            template.SetDescription("Project templates");

            template.AddCommand<ListTemplatesCommand>("list")
                .WithAlias("l")
                .WithDescription("List available project templates");
        });
    }).WithAlias("proj");
});

return app.Run(args);