using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Stellar.Sdk.Tasks
{
    public class StellarParseCSProjectFile : Task
    {
        [Required] public string ProjectDirectory { get; set; }
        [Required] public string AssemblyName { get; set; }

        // Выходные параметры
        [Output] public ITaskItem[] StellarDependenciesAsProject { get; set; }
        [Output] public ITaskItem[] StellarDependenciesAsPackages { get; set; }

        public override bool Execute()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError($"Error parsing Stellar configuration: {ex}");
                return false;
            }
        }
    }
}