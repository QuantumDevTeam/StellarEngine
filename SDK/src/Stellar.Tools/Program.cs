using Spectre.Console.Cli;
using Stellar.Tools.Commands;
using Stellar.Tools.Commands.Project;
using Stellar.Tools.Commands.Sdk;

var app = new CommandApp();

app.Configure(config =>
{
    config.SetApplicationName("stellar");

    config.AddCommand<InfoCommand>("info")
        .WithDescription("Show StellarEngine information");

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

        project.AddBranch("template", template =>
        {
            template.SetDescription("Project templates");

            template.AddCommand<ListTemplatesCommand>("list")
                .WithDescription("List available project templates");
        });
    });
});

return app.Run(args);