using System.Diagnostics;

namespace Stellar.Tools;

public sealed class StellarEnvironment
{
    public string GetDotnetFeatureBand()
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

    public string GetSdkAdvertisingPath()
    {
        var band = GetDotnetFeatureBand();
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        return Path.Combine(
            home,
            ".dotnet",
            "sdk-advertising",
            band,
            "stellar.sdk"
        );
    }
    
    public string GetSdkPackagePath()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        return Path.Combine(
            home,
            ".nuget",
            "packages",
            "stellar.sdk"
        );
    }

    public string? GetEnginePath()
    {
        return Environment.GetEnvironmentVariable("STELLAR_ENGINE_PATH");
    }
    
    public string? GetStellarPath()
    {
        return Environment.GetEnvironmentVariable("STELLAR_PATH");
    }
}