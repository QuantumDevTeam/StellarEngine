// ReSharper disable UnusedAutoPropertyAccessor.Global

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json;
using Stellar.Kernel.Configuration;
using Stellar.Kernel.Configuration.Project.Assets;
using Stellar.Kernel.Configuration.Project.Localization;

namespace Stellar.Sdk.Tasks
{
    public class StellarConfigGenerator : Task
    {
        public readonly string StellarRuntimeConfigName = ".stellar.runtime.config.json";

        // inputs

        [Required] public string IntermediateOutputPath { get; set; }
        [Required] public string StellarRuntimeConfigType { get; set; }

        // runtime config static
        [Required] public string StellarProjectName { get; set; }
        [Required] public string CompanyName { get; set; }
        [Required] public string Version { get; set; }
        [Required] public string StellarOrchesterVersion { get; set; }
        [Required] public string StellarEngineVersion { get; set; }
        [Required] public string StellarEntryPoint { get; set; }

        // engine components

        // asset component
        public ITaskItem[] Assets { get; set; }
        public ITaskItem[] EmbeddedAssets { get; set; }

        // localization component
        public string DefaultCulture { get; set; }
        public ITaskItem[] SupportedCultures { get; set; }
        public ITaskItem[] LocalizationIndexFiles { get; set; }

        // output

        [Output] public ITaskItem StellarRuntimeConfigFile { get; set; }

        private RuntimeConfiguration GenerateConfig()
        {
            RuntimeConfiguration config = new RuntimeConfiguration
            {
                ProjectName = StellarProjectName,
                CompanyName = CompanyName ?? string.Empty,
                Version = Version ?? "1.0.0",
                StellarOrchesterVersion = StellarOrchesterVersion ?? "0.0.0",
                StellarEngineVersion = StellarEngineVersion ?? "0.0.0",
                EntryPoint = StellarEntryPoint ?? "default",
                BuildDate = DateTime.UtcNow.ToString("O")
            };

            var components = new LinkedList<ConfigurationComponent>();

            components.AddLast(new AssetsComponent
            {
                EmbeddedAssets = EmbeddedAssets?
                    .Select(item => new AssetData
                    {
                        Path = item.GetMetadata("LogicalName"),
                        OriginalPath = item.GetMetadata("StellarRelativePath")
                    })
                    .ToArray() ?? Array.Empty<AssetData>(),

                ExternalAssets = Assets?
                    .Select(item => new AssetData
                    {
                        Path = item.GetMetadata("StellarRelativePath"),
                        OriginalPath = item.GetMetadata("StellarRelativePath")
                    })
                    .ToArray() ?? Array.Empty<AssetData>()
            });
            components.AddLast(new LocalizationComponent
            {
                DefaultCulture = DefaultCulture ?? "en",

                SupportedCultures = SupportedCultures?
                    .Select(item => item.ItemSpec)
                    .ToArray() ?? Array.Empty<string>(),

                LocalizationIndexFiles = LocalizationIndexFiles?
                    .Select(item => new LocalizationIndexData
                    {
                        Path = item.GetMetadata("StellarRelativePath"),
                        OriginalPath = item.GetMetadata("StellarRelativePath"),
                        Culture = item.GetMetadata("StellarCulture")
                    })
                    .ToArray() ?? Array.Empty<LocalizationIndexData>()
            });

            // TODO: Add project runtime components

            config.Components = components.ToArray();

            return config;
        }

        public override bool Execute()
            => Extensions.TryExecute(Log, "Generation runtime configuration failed: {0}", () =>
            {
                Log.LogMessage(MessageImportance.High,
                    $" Generating Stellar configuration for project: {StellarProjectName}");

                var config = GenerateConfig();

                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                var runtimeConfigName = $"{StellarProjectName}{StellarRuntimeConfigName}";
                var configIntermediateFile = Path.Combine(IntermediateOutputPath, runtimeConfigName);

                Directory.CreateDirectory(IntermediateOutputPath);
                File.WriteAllText(configIntermediateFile, json);

                Log.LogMessage(MessageImportance.High,
                    $" Stellar runtime configuration generated in file: {configIntermediateFile}");

                StellarRuntimeConfigFile = new TaskItem(configIntermediateFile);
                StellarRuntimeConfigFile.SetMetadata("Link", runtimeConfigName);

                if (StellarRuntimeConfigType == "Embedded")
                {
                    StellarRuntimeConfigFile.SetMetadata("LogicalName", $"{StellarProjectName}:{runtimeConfigName}");
                    StellarRuntimeConfigFile.SetMetadata("CopyToOutputDirectory", "Never");
                    StellarRuntimeConfigFile.SetMetadata("Embedded", "true");
                }
                else
                {
                    StellarRuntimeConfigFile.SetMetadata("CopyToOutputDirectory", "PreserveNewest");
                    StellarRuntimeConfigFile.SetMetadata("Embedded", "false");
                }

                return true;
            });
    }
}