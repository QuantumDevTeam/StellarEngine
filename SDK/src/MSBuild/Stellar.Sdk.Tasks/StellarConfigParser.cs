// ReSharper disable UnusedAutoPropertyAccessor.Global

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GlobExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Stellar.Sdk.Tasks
{
    public class AssetConfig
    {
        public string[] Include { get; set; } = Array.Empty<string>();
        public string[] Exclude { get; set; } = Array.Empty<string>();
        public string[] Embedded { get; set; } = Array.Empty<string>();
    }

    public class LocalizationConfig
    {
        public string DefaultCulture { get; set; } = "en";
        public string[] Cultures { get; set; } = Array.Empty<string>();
        public string[] IndexFiles { get; set; } = Array.Empty<string>();
    }

    public class ProjectConfigObject
    {
        public AssetConfig Assets { get; set; }
        public LocalizationConfig Localizations { get; set; }
    }

    public class StellarConfigFile
    {
        // представление .stellar.project[Project] поля
        public ProjectConfigObject ProjectConfig { get; set; }

        // представление .stellar.project[Runtime] поля
        public Dictionary<string, JsonObjectContract> RuntimeConfig { get; set; }
    }

    public class StellarConfigParser : Task
    {
        // Входные параметры
        [Required] public string ProjectDirectory { get; set; }
        [Required] public string StellarProjectName { get; set; }
        [Required] public string StellarProjectConfigurationFile { get; set; }
        [Required] public string StellarLocalizationIndexType { get; set; }

        // Выходные параметры
        [Output] public ITaskItem[] Assets { get; set; }
        [Output] public ITaskItem[] EmbeddedAssets { get; set; }

        [Output] public ITaskItem DefaultCulture { get; set; }
        [Output] public ITaskItem[] SupportedCultures { get; set; }
        [Output] public ITaskItem[] LocalizationIndexFiles { get; set; }

        [Output] public ITaskItem[] RuntimeConfig { get; set; } // TODO: export runtime part of project file

        private string GetRelativePath(string fullPath, string basePath)
        {
            fullPath = Path.GetFullPath(fullPath);
            basePath = Path.GetFullPath(basePath);

            if (!fullPath.StartsWith(basePath))
                throw new ArgumentException($"Path '{fullPath}' is not under base path '{basePath}'");

            if (fullPath == basePath)
                return string.Empty;

            var relativePath = fullPath.Substring(basePath.Length);
            return relativePath.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        private List<string> GetFilesByPatterns(string[] includePatterns, string[] excludePatterns)
        {
            var files = new List<string>();

            if (includePatterns == null || includePatterns.Length == 0)
                return files;

            var includeGlobs = includePatterns
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => new Glob(p))
                .ToList();

            var excludeGlobs = excludePatterns?
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => new Glob(p))
                .ToList() ?? new List<Glob>();

            var allFiles = Directory.GetFiles(ProjectDirectory, "*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                var relativePath = GetRelativePath(file, ProjectDirectory).Replace('\\', '/');

                bool isIncluded = includeGlobs.Any(glob => glob.IsMatch(relativePath));
                if (!isIncluded)
                    continue;

                bool isExcluded = excludeGlobs.Any(glob => glob.IsMatch(relativePath));
                if (isExcluded)
                    continue;

                files.Add(file);
            }

            return files;
        }

        private string GetLogicalName(string fileRelativePath)
        {
            var logicalName = fileRelativePath.Replace('\\', '/');
            return $"{StellarProjectName}:{logicalName}";
        }

        private ITaskItem CreateAssetItem(string filePath, bool isEmbedded)
        {
            var item = new TaskItem(filePath);
            var relativePath = GetRelativePath(filePath, ProjectDirectory);

            item.SetMetadata("Link", relativePath);

            item.SetMetadata("StellarFullPath", filePath);
            item.SetMetadata("StellarRelativePath", relativePath);

            if (isEmbedded)
            {
                item.SetMetadata("LogicalName", GetLogicalName(relativePath));
                item.SetMetadata("CopyToOutputDirectory", "Never");
                item.SetMetadata("Embedded", "true");
            }
            else
            {
                item.SetMetadata("CopyToOutputDirectory", "PreserveNewest");
                item.SetMetadata("Embedded", "false");
            }

            return item;
        }

        private ITaskItem CreateLocalizationItem(string filePath, bool isEmbedded)
        {
            var item = CreateAssetItem(filePath, isEmbedded);
            var culture = Path.GetFileName(filePath).Replace(".stellar.loc.index", "");

            item.SetMetadata("StellarCulture", culture);

            return item;
        }

        private bool ProcessAssets(ProjectConfigObject config) =>
            Extensions.TryExecute(Log, "Error processing Stellar assets: {0}", () =>
            {
                // генерация встроенные ассетов

                List<string> embeddedAssetFiles = GetFilesByPatterns(config.Assets.Embedded, config.Assets.Exclude);
                Log.LogMessage(MessageImportance.High, $" Found {embeddedAssetFiles.Count} embedded assets");

                EmbeddedAssets = embeddedAssetFiles
                    .Select(file => CreateAssetItem(file, isEmbedded: true))
                    .ToArray();

                // генерация обычных ассетов

                var embeddedRelativePaths = embeddedAssetFiles
                    .Select(f => GetRelativePath(f, ProjectDirectory).Replace('\\', '/'))
                    .ToArray();
                var allExcludePatterns = config.Assets.Exclude
                    .Concat(embeddedRelativePaths)
                    .ToArray();

                var externalAssetFiles = GetFilesByPatterns(config.Assets.Include, allExcludePatterns);
                Log.LogMessage(MessageImportance.High, $" Found {externalAssetFiles.Count} external assets");

                Assets = externalAssetFiles
                    .Select(file => CreateAssetItem(file, isEmbedded: false))
                    .ToArray();

                return true;
            });

        private bool ProcessLocalizations(ProjectConfigObject config) =>
            Extensions.TryExecute(Log, "Error processing Stellar localizations: {0}", () =>
            {
                if (!config.Localizations.Cultures.Contains(config.Localizations.DefaultCulture))
                {
                    Log.LogError(
                        $"Default culture '{config.Localizations.DefaultCulture}' is not in the list of " +
                        $"supported cultures: {string.Join(", ", config.Localizations.Cultures)}"
                    );
                    return false;
                }

                var localizationFiles = GetFilesByPatterns(config.Localizations.IndexFiles, Array.Empty<string>());
                Log.LogMessage(MessageImportance.High, $" Found {localizationFiles.Count} localization index files");

                DefaultCulture = new TaskItem(config.Localizations.DefaultCulture);

                SupportedCultures = config.Localizations.Cultures
                    .Select(c => new TaskItem(c))
                    .ToArray<ITaskItem>();

                LocalizationIndexFiles = localizationFiles
                    .Select(file => CreateLocalizationItem(file, StellarLocalizationIndexType == "Embedded"))
                    .ToArray();

                return true;
            });

        public override bool Execute() =>
            Extensions.TryExecute(Log, "Error Parsing Stellar project file: {0}", () =>
            {
                Debug.Assert(ProjectDirectory != null, "ProjectDirectory must be present");
                Debug.Assert(StellarProjectName != null, "StellarProjectName must be present");
                Debug.Assert(StellarProjectConfigurationFile != null,
                    "StellarProjectConfigurationFile must be present");
                Debug.Assert(StellarLocalizationIndexType != null, "StellarLocalizationIndexType must be present");

                var configFilePath = Path.Combine(ProjectDirectory, StellarProjectConfigurationFile);

                Log.LogMessage(MessageImportance.High,
                    $" Parsing project configuration: {Path.GetFileName(configFilePath)}"
                );

                if (!File.Exists(configFilePath))
                {
                    Log.LogError($"Configuration file not found: {configFilePath}");
                    return false;
                }

                var configJson = File.ReadAllText(configFilePath);
                var config = JsonConvert.DeserializeObject<StellarConfigFile>(configJson).ProjectConfig;

                if (!ProcessAssets(config)) return false;
                if (!ProcessLocalizations(config)) return false;

                return true;
            });
    }
}