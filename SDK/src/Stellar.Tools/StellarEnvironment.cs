using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

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

    public static string StellarToolsVersion => Assembly.GetExecutingAssembly()
        .GetCustomAttributes<AssemblyMetadataAttribute>()
        .FirstOrDefault(a => a.Key == "StellarToolsVersion"
        )?.Value ?? throw new KeyNotFoundException("StellarToolsVersion not found in AssemblyInfo");

    public static string GetStellarSdkVersion()
    {
        var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
    
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
    
        var json = File.ReadAllText(filePath);
        using var document = JsonDocument.Parse(json);
    
        if (document.RootElement.TryGetProperty("SdkVersion", out var versionElement))
        {
            return versionElement.GetString() ?? string.Empty;
        }
    
        throw new KeyNotFoundException("SdkVersion not found in JSON");
    }
    
    public static string GetStellarKernelVersion()
    {
        var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
    
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
    
        var json = File.ReadAllText(filePath);
        using var document = JsonDocument.Parse(json);
    
        if (document.RootElement.TryGetProperty("KernelVersion", out var versionElement))
        {
            return versionElement.GetString() ?? string.Empty;
        }
    
        throw new KeyNotFoundException("KernelVersion not found in JSON");
    }
    
    public static string GetStellarEngineVersion()
    {
        var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
    
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
    
        var json = File.ReadAllText(filePath);
        using var document = JsonDocument.Parse(json);
    
        if (document.RootElement.TryGetProperty("EngineVersion", out var versionElement))
        {
            return versionElement.GetString() ?? string.Empty;
        }
    
        throw new KeyNotFoundException("EngineVersion not found in JSON");
    }
}