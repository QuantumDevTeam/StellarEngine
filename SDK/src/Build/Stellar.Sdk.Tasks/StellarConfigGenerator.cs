using System;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json;

namespace Stellar.Sdk.Tasks
{
    public class StellarProjectConfig
    {
        public string ProjectName { get; set; }
        public string CompanyName { get; set; }

        public string Version { get; set; }
        public string SdkVersion { get; set; }
        public string EngineVersion { get; set; }

        public string EntryPoint { get; set; }
        public bool RuntimeConfigIsEmbedded { get; set; }

        public string BuildDate { get; set; }

        public AssetsConfigData Assets { get; set; }
    }

    public class AssetsConfigData
    {
        public AssetInfo[] EmbeddedAssets { get; set; }
        public AssetInfo[] ExternalAssets { get; set; }
    }

    public class AssetInfo
    {
        public string Path { get; set; }
        public string OriginalPath { get; set; }
    }

    public class GameConfig : StellarProjectConfig
    {
        public bool LocalizationIndexFilesIsEmbedded { get; set; }
        public LocalizationConfigData Localization { get; set; }
    }

    public class LocalizationConfigData
    {
        public string DefaultCulture { get; set; }
        public string[] SupportedCultures { get; set; }
        public CultureFileInfo[] CultureIndexFiles { get; set; }
    }

    public class CultureFileInfo : AssetInfo
    {
        public string Culture { get; set; }
    }

    public class StellarConfigGenerator : Task
    {
        public readonly string StellarRuntimeConfigName = ".stellar.runtime.config.json";

        // Входные параметры
        [Required] public string IntermediateOutputPath { get; set; }
        [Required] public string StellarSdkUsage { get; set; }

        [Required] public string StellarProjectName { get; set; }
        [Required] public string CompanyName { get; set; }

        [Required] public string Version { get; set; }
        [Required] public string StellarSdkVersion { get; set; }
        [Required] public string StellarEngineVersion { get; set; }

        [Required] public ITaskItem StellarEntryPoint { get; set; }

        [Required] public ITaskItem[] StellarAssets { get; set; }
        [Required] public ITaskItem[] StellarEmbeddedAssets { get; set; }

        [Required] public ITaskItem[] StellarSupportedCultures { get; set; }
        [Required] public ITaskItem[] StellarCultureIndexFilesList { get; set; }
        [Required] public ITaskItem StellarDefaultCulture { get; set; }

        [Required] public string StellarLocalizationIndexType { get; set; }
        [Required] public string StellarRuntimeConfigType { get; set; }

        [Output] public ITaskItem StellarRuntimeConfigFile { get; set; }

        public override bool Execute()
        {
            try
            {
                Log.LogMessage(MessageImportance.High,
                    $" Generating Stellar configuration for project: {StellarProjectName}");

                var config = GenerateConfig();

                var json = JsonConvert.SerializeObject(config, Formatting.Indented);

                Directory.CreateDirectory(IntermediateOutputPath);

                var configIntermediateFile = Path.Combine(IntermediateOutputPath, StellarRuntimeConfigName);
                File.WriteAllText(configIntermediateFile, json);

                Log.LogMessage(MessageImportance.High,
                    $" Stellar runtime configuration generated in file: {configIntermediateFile}");

                StellarRuntimeConfigFile = new TaskItem(configIntermediateFile);

                StellarRuntimeConfigFile.SetMetadata("Link", StellarRuntimeConfigName);

                if (config.RuntimeConfigIsEmbedded)
                {
                    StellarRuntimeConfigFile.SetMetadata("LogicalName",
                        $"{StellarProjectName}:{StellarRuntimeConfigName}");
                    StellarRuntimeConfigFile.SetMetadata("CopyToOutputDirectory", "Never");
                    StellarRuntimeConfigFile.SetMetadata("Embedded", "true");
                }
                else
                {
                    StellarRuntimeConfigFile.SetMetadata("CopyToOutputDirectory", "PreserveNewest");
                    StellarRuntimeConfigFile.SetMetadata("Embedded", "false");
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogError($"Error generating Stellar configuration: {ex}");
                return false;
            }
        }

        private StellarProjectConfig GenerateConfig()
        {
            StellarProjectConfig config;

            switch (StellarSdkUsage)
            {
                case "Game":
                    var localizationIndexFilesIsEmbedded = StellarLocalizationIndexType == "Embedded";
                    config = new GameConfig()
                    {
                        LocalizationIndexFilesIsEmbedded = localizationIndexFilesIsEmbedded,
                        Localization = new LocalizationConfigData
                        {
                            DefaultCulture = StellarDefaultCulture.ItemSpec,
                            SupportedCultures = StellarSupportedCultures
                                .Select(item => item.ItemSpec)
                                .ToArray(),
                            CultureIndexFiles = StellarCultureIndexFilesList?
                                .Select(item => new CultureFileInfo
                                {
                                    Path = localizationIndexFilesIsEmbedded
                                        ? item.GetMetadata("LogicalName")
                                        : item.GetMetadata("StellarRelativePath"),
                                    OriginalPath = item.GetMetadata("StellarRelativePath"),
                                    Culture = item.GetMetadata("StellarCulture")
                                })
                                .ToArray() ?? Array.Empty<CultureFileInfo>()
                        }
                    };
                    break;
                case "EngineModule":
                case "Module":
                    config = new StellarProjectConfig();
                    break;
                default:
                    throw new NotSupportedException("Unknown StellarSdkUsage");
            }

            config.ProjectName = StellarProjectName;
            config.CompanyName = CompanyName ?? string.Empty;
            config.Version = Version ?? "1.0.0";
            config.SdkVersion = StellarSdkVersion ?? "0.0.0";
            config.EngineVersion = StellarEngineVersion ?? "0.0.0";
            config.BuildDate = DateTime.UtcNow.ToString("O");
            config.EntryPoint = StellarEntryPoint.ItemSpec;
            config.RuntimeConfigIsEmbedded = StellarRuntimeConfigType == "Embedded";
            config.Assets = new AssetsConfigData
            {
                EmbeddedAssets = StellarEmbeddedAssets?
                    .Select(item => new AssetInfo
                    {
                        Path = item.GetMetadata("LogicalName"),
                        OriginalPath = item.GetMetadata("StellarRelativePath")
                    })
                    .ToArray() ?? Array.Empty<AssetInfo>(),

                ExternalAssets = StellarAssets?
                    .Select(item => new AssetInfo
                    {
                        Path = item.GetMetadata("StellarRelativePath"),
                        OriginalPath = item.GetMetadata("StellarRelativePath")
                    })
                    .ToArray() ?? Array.Empty<AssetInfo>()
            };

            return config;
        }
    }
}