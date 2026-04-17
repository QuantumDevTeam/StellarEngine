using System.Diagnostics;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Stellar.Core;

public static class Extensions
{
    // TODO: Extensions
    public static IEnumerable<Module> TraceModules()
    {
        var stackTrace = new StackTrace(skipFrames: 0, fNeedFileInfo: true);
        
        foreach (var stackFrame in stackTrace.GetFrames())
        {
            if (stackFrame.GetMethod()?.Module is {} module) yield return module;
            yield break;
        }
    }
}