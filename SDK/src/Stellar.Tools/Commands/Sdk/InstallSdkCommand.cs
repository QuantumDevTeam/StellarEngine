using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands.Sdk;

public sealed class InstallSdkCommand : Command
{
    private int CreateSharedDirectory()
    {
        Directory.CreateDirectory(StellarEnvironment.GetStellarEngineSharedPath());
        Directory.CreateDirectory(Path.Combine(StellarEnvironment.GetStellarEngineSharedPath(), "projects"));

        File.WriteAllText(
            Path.Combine(StellarEnvironment.GetStellarEngineSharedPath(), "installation_location.txt"),
            StellarEnvironment.WorkingDirectory
        );

        var descPath = Path.Combine(StellarEnvironment.GetStellarEngineSharedPath(), ".stellar.desc.json");
        if (!File.Exists(descPath))
            File.Copy(
                Path.Combine(StellarEnvironment.GetStellarEngineInstallationPath(), "Data", ".generated",
                    ".stellar.desc.json"),
                descPath
            );
        return 0;
    }

    private int WorkloadManifestCopying()
    {
        var engineInstallationDir = StellarEnvironment.GetStellarEngineInstallationPath();
        var manifestDir = StellarEnvironment.GetSdkAdvertisingPath();
        var featureBand = StellarEnvironment.GetDotnetFeatureBand();

        var source = Path.Combine(engineInstallationDir, "Data", ".generated", "stellar-workload");

        AnsiConsole.MarkupLine($"[grey]Feature band:[/] {featureBand}");
        AnsiConsole.MarkupLine($"[grey]SDK Manifest Source dir:[/] {source}");
        AnsiConsole.MarkupLine($"[grey]Installing SDK Manifest to:[/] {manifestDir}");

        Directory.CreateDirectory(manifestDir);

        if (!Directory.Exists(source))
        {
            AnsiConsole.MarkupLine("[red]Workload artifacts not found[/]");
            return -1;
        }

        foreach (var file in Directory.GetFiles(source))
        {
            var dest = Path.Combine(manifestDir, Path.GetFileName(file));
            File.Copy(file, dest, overwrite: true);
        }

        File.WriteAllText(Path.Combine(manifestDir, "AdvertisedManifestFeatureBand.txt"), featureBand);
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