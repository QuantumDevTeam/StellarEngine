using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands.Sdk;

public sealed class InstallSdkCommand : Command
{
    private int CreateSharedDirectory()
    {
        var orchesterSharedDir = StellarEnvironment.StellarOrchesterSharedDir;
        var orchesterInstallationDir = StellarEnvironment.WorkingDirectory;
        var descPath = Path.Combine(orchesterSharedDir, ".stellar.desc.json");

        AnsiConsole.MarkupLine($"[grey]Orchester dir:[/] {orchesterInstallationDir}");
        AnsiConsole.MarkupLine($"[grey]Orchester shared dir:[/] {orchesterSharedDir}");

        Directory.CreateDirectory(orchesterSharedDir);

        File.WriteAllText(
            Path.Combine(orchesterSharedDir, "installation_location.txt"),
            orchesterInstallationDir
        );

        File.Copy(
            Path.Combine(StellarEnvironment.StellarOrchesterInstallationDir, "Data", ".generated",
                ".stellar.desc.json"),
            descPath,
            true
        );

        Directory.CreateDirectory(Path.Combine(orchesterSharedDir, "projects"));
        return 0;
    }

    private int WorkloadManifestCopying()
    {
        var orchesterInstallationDir = StellarEnvironment.StellarOrchesterInstallationDir;
        var featureBand = StellarEnvironment.GetDotnetFeatureBand();
        var sdkManifestDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".dotnet",
            "sdk-advertising",
            featureBand,
            "stellar.sdk"
        );
        var sdkManifestSourceDir = Path.Combine(orchesterInstallationDir, "Data", ".generated", "stellar-workload");

        AnsiConsole.MarkupLine($"[grey]Feature band:[/] {featureBand}");
        AnsiConsole.MarkupLine($"[grey]SDK Manifest Source dir:[/] {sdkManifestSourceDir}");
        AnsiConsole.MarkupLine($"[grey]Installing SDK Manifest to:[/] {sdkManifestDir}");

        Directory.CreateDirectory(sdkManifestDir);

        if (!Directory.Exists(sdkManifestSourceDir))
        {
            AnsiConsole.MarkupLine("[red]Workload artifacts not found[/]");
            return -1;
        }

        foreach (var file in Directory.GetFiles(sdkManifestSourceDir))
        {
            var dest = Path.Combine(sdkManifestDir, Path.GetFileName(file));
            File.Copy(file, dest, overwrite: true);
        }

        File.WriteAllText(Path.Combine(sdkManifestDir, "AdvertisedManifestFeatureBand.txt"), featureBand);
        return 0;
    }

    public override int Execute(CommandContext context)
    {
        var sharedDirectoryExitCode = CreateSharedDirectory();
        if (sharedDirectoryExitCode != 0) return sharedDirectoryExitCode;

        var workloadExitCode = WorkloadManifestCopying();
        if (workloadExitCode != 0) return workloadExitCode;

        AnsiConsole.MarkupLine("[green]Stellar SDK installed successfully[/]");
        return 0;
    }
}