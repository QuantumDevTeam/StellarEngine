using Spectre.Console.Cli;
using Stellar.CLI.Commands;
using Stellar.CLI.Commands.Project;
using Stellar.CLI.Commands.Sdk;
using Stellar.CLI.Commands.Version;

var app = new CommandApp();

app.Configure(config =>
{
    config.SetApplicationName("stellar");

    config.AddCommand<InfoCommand>("info")
        .WithDescription("Show StellarEngine information");

    config.AddBranch("version", version =>
    {
        version.SetDescription("Systems versions");

        version.AddCommand<OrchesterVersionCommand>("orchester")
            .WithDescription("Tools Version");

        version.AddCommand<KernelVersionCommand>("kernel")
            .WithDescription("Kernel Version");

        version.AddCommand<ToolsVersionCommand>("tools")
            .WithDescription("Tools Version");

        version.AddCommand<SdkVersionCommand>("sdk")
            .WithDescription("SDK Version");

        version.AddCommand<SdkVersionCommand>("cli")
            .WithDescription("SDK Version");

        version.AddCommand<EngineVersionCommand>("engine")
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
            .WithDescription("Create new project from template");

        project.AddCommand<GenerateProjectDocsCommand>("generate-docs")
            .WithDescription("Show project information");

        project.AddBranch("template", template =>
        {
            template.SetDescription("Project templates");

            template.AddCommand<ListTemplatesCommand>("list")
                .WithDescription("List available project templates");
        });
    }).WithAlias("proj");
});

return app.Run(args);