using System.Diagnostics;
using System.Reflection;

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

    public static string StellarToolsVersion => Assembly.GetExecutingAssembly()
        .GetCustomAttributes<AssemblyVersionAttribute>()
        .FirstOrDefault()?.Version ?? throw new KeyNotFoundException("AssemblyVersion not found in AssemblyInfo");

    public static string StellarOrchesterVersion => Assembly.GetExecutingAssembly()
        .GetCustomAttributes<AssemblyMetadataAttribute>()
        .FirstOrDefault(a => a.Key == "StellarOrchesterVersion"
        )?.Value ?? throw new KeyNotFoundException("StellarOrchesterVersion not found in AssemblyInfo");

    public static string StellarOrchesterSharedDir => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".stellar",
        StellarOrchesterVersion
    );

    public static string StellarOrchesterInstallationDir => File.ReadAllText(Path.Combine(
        StellarOrchesterSharedDir,
        "installation_location.txt"
    ));
}