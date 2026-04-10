using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Stellar.Tools
{
    public static class StellarEnvironment
    {
        public static string GetDotnetFeatureBand()
        {
            var psi = new ProcessStartInfo("dotnet", "--version")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using (var p = Process.Start(psi))
            {
                if (p == null) throw new InvalidOperationException("Failed to start dotnet process");
                var line = p.StandardOutput.ReadLine();
                if (string.IsNullOrEmpty(line)) throw new InvalidOperationException("dotnet --version returned empty");
                var version = line.Trim();
                var parts = version.Split('.');
                return $"{parts[0]}.{parts[1]}.{parts[2].Split('-')[0]}";
            }
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
        
        private static string GetJsonProperty(string filePath, string propertyName)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var json = File.ReadAllText(filePath);
            var searchKey = $"\"{propertyName}\":";
            var index = json.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase);
            if (index == -1)
                throw new KeyNotFoundException($"'{propertyName}' not found in JSON");

            var start = index + searchKey.Length;
            while (start < json.Length && (json[start] == ' ' || json[start] == '\t' || json[start] == '\r' || json[start] == '\n'))
                start++;

            if (start >= json.Length) throw new KeyNotFoundException($"No value for '{propertyName}'");

            char quote = json[start];
            if (quote == '"')
            {
                var end = start + 1;
                while (end < json.Length && json[end] != '"')
                {
                    if (json[end] == '\\') end++;
                    end++;
                }
                return json.Substring(start + 1, end - start - 1);
            }
            else
            {
                var end = start;
                while (end < json.Length && json[end] != ',' && json[end] != '}' && json[end] != '\r' && json[end] != '\n')
                    end++;
                return json.Substring(start, end - start).Trim();
            }
        }
        
        public static string GetStellarKernelVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "KernelVersion");
        }
        
        public static string GetStellarToolsVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "ToolsVersion");
        }

        public static string GetStellarSdkVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "SdkVersion");
        }

        public static string GetStellarCliVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "CliVersion");
        }

        public static string GetStellarEngineVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "EngineVersion");
        }
    }
}