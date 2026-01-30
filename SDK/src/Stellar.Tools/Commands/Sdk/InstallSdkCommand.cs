using Spectre.Console;
using Spectre.Console.Cli;

namespace Stellar.Tools.Commands.Sdk;

public sealed class InstallSdkCommand : Command
{
    private int WorkloadManifestCopying(StellarEnvironment env)
    {
        var sdkDir = env.GetSdkPackagePath();
        var manifestDir = env.GetSdkAdvertisingPath();
        var featureBand = env.GetDotnetFeatureBand();

        AnsiConsole.MarkupLine($"[grey]Feature band:[/] {featureBand}");
        AnsiConsole.MarkupLine($"[grey]SDK dir:[/] {sdkDir}");
        AnsiConsole.MarkupLine($"[grey]Installing SDK Manifest to:[/] {manifestDir}");

        Directory.CreateDirectory(manifestDir);

        var source = Path.Combine(sdkDir, "data", "workload");
        if (!Directory.Exists(source))
        {
            AnsiConsole.MarkupLine("[red]Workload artifacts not found[/]");
            AnsiConsole.MarkupLine($"Expected path: {source}");
            return -1;
        }

        foreach (var file in Directory.GetFiles(source))
        {
            var dest = Path.Combine(manifestDir, Path.GetFileName(file));
            File.Copy(file, dest, overwrite: true);
        }

        File.WriteAllText(
            Path.Combine(manifestDir, "AdvertisedManifestFeatureBand.txt"), featureBand
        );
        return 0;
    }

    public override int Execute(CommandContext context)
    {
        var env = new StellarEnvironment();

        var workloadExitCode = WorkloadManifestCopying(env);
        if (workloadExitCode != 0) return workloadExitCode;

        Environment.SetEnvironmentVariable(
            "STELLAR_PATH", env.GetStellarPath(), EnvironmentVariableTarget.User
        );

        AnsiConsole.MarkupLine("[green]Stellar SDK installed successfully[/]");
        return 0;
    }
}