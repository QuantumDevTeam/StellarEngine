using System.Diagnostics;

namespace Stellar.Tools;

public static class StellarEnvironment
{
    public static string GetDotnetFeatureBand()
    {
        var psi = new ProcessStartInfo("dotnet", "--version")
        {
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        using var p = Process.Start(psi)!;
        var version = p.StandardOutput.ReadLine()!.Trim();

        var parts = version.Split('.');
        return $"{parts[0]}.{parts[1]}.{parts[2].Split('-')[0]}";
    }

    public static string WorkingDirectory => Environment.CurrentDirectory;

    public static string GetSdkAdvertisingPath() => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".dotnet",
        "sdk-advertising",
        GetDotnetFeatureBand(),
        "stellar.sdk"
    );

    public static string GetSdkPackagePath() => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".nuget",
        "packages",
        "stellar.sdk"
    );

    public static string GetStellarEngineSharedPath() => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".stellar",
        StellarVersions.EngineVersion
    );

    public static string GetStellarEngineInstallationPath() => File.ReadAllText(Path.Combine(
        GetStellarEngineSharedPath(),
        "installation_location.txt"
    ));
}