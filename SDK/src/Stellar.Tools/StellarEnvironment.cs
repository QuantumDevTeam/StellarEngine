using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Stellar.Tools
{
    /// <summary>
    /// Provide base Stellar features
    /// </summary>
    public static class StellarEnvironment
    {
        /// <summary>
        /// Get Dotnet feature band
        /// </summary>
        /// <returns>version of dotnet</returns>
        /// <exception cref="InvalidOperationException">if an operation in tools has been invalid</exception>
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

        /// <summary>
        /// Current WorkingDirectory
        /// </summary>
        public static string WorkingDirectory => Environment.CurrentDirectory;

        /// <summary>
        /// Gets global StellarOrchesterVersion
        /// </summary>
        /// <exception cref="KeyNotFoundException">if StellarOrchesterVersion not found in assembly info</exception>
        public static string StellarOrchesterVersion => Assembly.GetExecutingAssembly()
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(a => a.Key == "StellarOrchesterVersion"
            )?.Value ?? throw new KeyNotFoundException("StellarOrchesterVersion not found in AssemblyInfo");

        /// <summary>
        /// Gets StellarOrchesterSharedDir
        /// </summary>
        public static string StellarOrchesterSharedDir => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".stellar",
            StellarOrchesterVersion
        );

        /// <summary>
        /// Gets StellarOrchesterInstallationDir
        /// </summary>
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
        
        /// <summary>
        /// Give version of Kernel
        /// </summary>
        /// <returns>Kernel Version</returns>
        public static string GetStellarKernelVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "KernelVersion");
        }
        
        /// <summary>
        /// Give version of Tools
        /// </summary>
        /// <returns>Tools Version</returns>
        public static string GetStellarToolsVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "ToolsVersion");
        }

        /// <summary>
        /// Give version of SDK
        /// </summary>
        /// <returns>SDK Version</returns>
        public static string GetStellarSdkVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "SdkVersion");
        }

        /// <summary>
        /// Give version of CLI
        /// </summary>
        /// <returns>CLI Version</returns>
        public static string GetStellarCliVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "CliVersion");
        }

        /// <summary>
        /// Give version of Engine Runtime Modules
        /// </summary>
        /// <returns>Engine Runtime Modules Version</returns>
        public static string GetStellarEngineVersion()
        {
            var filePath = Path.Combine(StellarOrchesterSharedDir, ".stellar.desc.json");
            return GetJsonProperty(filePath, "EngineVersion");
        }
    }
}