// ReSharper disable UnusedAutoPropertyAccessor.Global

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Stellar.Sdk.Tasks
{
    // ReSharper disable once UnusedType.Global
    public class StellarParseCSProjectFile : Task
    {
        [Required] public string ProjectFile { get; set; }
        [Required] public bool StellarPrecompiled { get; set; }
        [Required] public string StellarEngineVersion { get; set; }
        [Required] public string StellarOrchesterInstallationDir { get; set; }

        [Output] public ITaskItem[] StellarDependenciesAsProject { get; set; }
        [Output] public ITaskItem[] StellarDependenciesAsPackages { get; set; }

        private static string GetAttributeValue(XElement element, string attributeName)
        {
            XAttribute attr = element.Attribute(attributeName);
            return attr?.Value;
        }

        private bool FindStellarDeps(XElement root) =>
            Extensions.TryExecute(Log, "Error parsing Stellar Dependencies: {0}", () =>
            {
                Log.LogMessage(MessageImportance.Normal, $"Parsing Stellar dependencies from {ProjectFile}");

                var stellarDeps = root.Descendants()
                    .Where(e => e.Name.LocalName == "StellarDependency")
                    .ToList();

                if (!stellarDeps.Any())
                {
                    Log.LogMessage("No StellarDependency elements found.");
                    StellarDependenciesAsProject = Array.Empty<ITaskItem>();
                    StellarDependenciesAsPackages = Array.Empty<ITaskItem>();
                    return true;
                }

                var projectItems = new List<ITaskItem>();
                var packageItems = new List<ITaskItem>();

                foreach (var element in stellarDeps)
                {
                    string include = GetAttributeValue(element, "Include");
                    if (string.IsNullOrEmpty(include))
                    {
                        Log.LogWarning("StellarDependency element has empty Include attribute. Skipping.");
                        continue;
                    }

                    string type = GetAttributeValue(element, "Type");

                    string usePrecompiledStr = GetAttributeValue(element, "UsePrecompiled");
                    bool usePrecompiled = !string.IsNullOrEmpty(usePrecompiledStr) && bool.Parse(usePrecompiledStr);

                    string privateAssets = GetAttributeValue(element, "PrivateAssets");

                    if (string.Equals(type, "EngineModule", StringComparison.Ordinal))
                    {
                        string moduleName = include;
                        bool engineUsePrecompiled = usePrecompiled || StellarPrecompiled;

                        if (!engineUsePrecompiled)
                        {
                            string projectPath = Path.Combine(
                                StellarOrchesterInstallationDir,
                                "Engine", "src", "Modules",
                                moduleName,
                                moduleName + ".csproj");
                            var item = new TaskItem(projectPath);

                            item.SetMetadata("PrivateAssets",
                                !string.IsNullOrEmpty(privateAssets) ? privateAssets : "all");

                            projectItems.Add(item);
                            Log.LogMessage(MessageImportance.Low, $"Added ProjectReference: {projectPath}");
                        }
                        else
                        {
                            var item = new TaskItem(moduleName);

                            item.SetMetadata("Version", StellarEngineVersion);
                            item.SetMetadata("PrivateAssets",
                                !string.IsNullOrEmpty(privateAssets) ? privateAssets : "all");

                            packageItems.Add(item);
                            Log.LogMessage(MessageImportance.Low,
                                $"Added PackageReference: {moduleName}, Version={StellarEngineVersion}");
                        }
                    }
                    else if (string.Equals(type, "Module", StringComparison.Ordinal))
                    {
                        string projectPath = include;

                        if (!usePrecompiled)
                        {
                            if (!projectPath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
                                projectPath += ".csproj";
                            var item = new TaskItem(projectPath);

                            if (!string.IsNullOrEmpty(privateAssets))
                                item.SetMetadata("PrivateAssets", privateAssets);

                            projectItems.Add(item);
                            Log.LogMessage(MessageImportance.Low, $"Added ProjectReference: {projectPath}");
                        }
                        else
                        {
                            string packageName = Path.GetFileName(projectPath);
                            var item = new TaskItem(packageName);

                            if (!string.IsNullOrEmpty(privateAssets))
                                item.SetMetadata("PrivateAssets", privateAssets);

                            packageItems.Add(item);
                            Log.LogMessage(MessageImportance.Low,
                                $"Added PackageReference: {packageName}, Version={StellarEngineVersion}");
                        }
                    }
                    else
                    {
                        Log.LogWarning("StellarDependency element has wrong Type. Skipping.");
                    }
                }

                StellarDependenciesAsProject = projectItems.ToArray();
                StellarDependenciesAsPackages = packageItems.ToArray();

                return true;
            });

        public override bool Execute() =>
            Extensions.TryExecute(Log, "Error parsing Stellar configuration: {0}", () =>
            {
                Debug.Assert(ProjectFile != null,
                    "ProjectDirectory must be present");
                
                if (!File.Exists(ProjectFile))
                {
                    Log.LogError($"Project file not found: {ProjectFile}");
                    return false;
                }

                var doc = XDocument.Load(ProjectFile);
                var root = doc.Descendants()
                    .Where(e => e.Name.LocalName == "ProjectExtensions")
                    .Elements();

                foreach (var xElement in root)
                {
                    if (!FindStellarDeps(xElement)) return false;
                }

                return true;
            });
    }
}