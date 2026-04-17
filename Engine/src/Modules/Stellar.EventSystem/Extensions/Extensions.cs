using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Stellar.EventSystem;

public class Extensions
{
    static short engine;
    static short application;
    
    private static IEnumerator<short> EventTypeValueEnumerator()
    {
        foreach (var module in Core.Extensions.TraceModules())
        {
            Console.WriteLine(module.Assembly.GetName().FullName);
        }

        if (new StackTrace(skipFrames: 0, fNeedFileInfo: true)
                .GetFrame(2)?
                .GetMethod()?
                .Module.Assembly
                .GetName().FullName
                .StartsWith("Stellar.") is true)
            yield return engine--;
        else
            yield return application++;
    }

    public static short GenerateEventTypeValue()
    {
        var v = EventTypeValueEnumerator().Current;
        EventTypeValueEnumerator().MoveNext();
        return v;
    }
}