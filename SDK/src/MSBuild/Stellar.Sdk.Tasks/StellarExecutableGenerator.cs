using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.NET.HostModel.AppHost;

namespace Stellar.Sdk.Tasks
{
    public class StellarExecutableGenerator : Task
    {
        [Required] public string StellarProjectName { get; set; }
        [Required] public string StellarOutputType { get; set; }
        [Required] public string StellarEnginePath { get; set; }


        [Required] public string OutputPath { get; set; }
        [Required] public string RunConfiguration { get; set; }

        private string GetBinaryDir(bool isEngine)
        {
            switch (StellarOutputType)
            {
                case "FlatRoot":
                    return "";
                case "Flat":
                    return "bin";
                case "Modern":
                    return isEngine ? "bin/Engine" : "bin";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool Execute()
        {
            try
            {
                Log.LogMessage(MessageImportance.High,
                    $"Creating AppHost for game: {StellarProjectName}");

                var gameDllPath = Path.Combine(GetBinaryDir(false), $"{StellarProjectName}.dll");
                string launcherDllPath = Path.Combine(GetBinaryDir(true), "StellarEngine.Launcher.dll");
                string outputExePath = Path.Combine(OutputPath, $"{StellarProjectName}.exe");
                string appHostTemplate = Path.Combine(StellarEnginePath, "Data", "apphost.exe");

                if (!File.Exists(appHostTemplate))
                {
                    Log.LogError($"AppHost template not found at: {appHostTemplate}");
                    return false;
                }

                HostWriter.CreateAppHost(
                    appHostSourceFilePath: appHostTemplate,
                    appHostDestinationFilePath: outputExePath,
                    appBinaryFilePath: launcherDllPath,
                    windowsGraphicalUserInterface: RunConfiguration == "Release",
                    assemblyToCopyResorcesFrom: gameDllPath
                );

                Log.LogMessage(MessageImportance.High,
                    $"AppHost created successfully: {outputExePath}");

                return true;
            }
            catch (Exception ex)
            {
                Log.LogError($"Error generating Executable file for Stellar game: {ex}");
                return true;
            }
        }
    }
}