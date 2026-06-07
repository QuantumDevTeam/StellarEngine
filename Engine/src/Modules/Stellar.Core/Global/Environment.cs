#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Stellar.Kernel.Data.Collections;
using Stellar.Kernel.Data.Registry;
using Stellar.Kernel.EntryPoint;
using Stellar.Kernel.Failures;
using Stellar.Kernel.FileSystem.Provider;
using Stellar.Kernel.LoggingSystem;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.JobSystem;

namespace Stellar.Core.Global;

public static class Engine
{
    // TODO: initialize all from constructor

    public static IFailureDispatcher FailureDispatcher { get; internal set; }
    public static IJobScheduler TaskScheduler { get; internal set; }
    public static IFileProviderFactory FileProviderFactory { get; internal set; }
    public static ILogger Logger { get; internal set; }

    public static class FailureLevels
    {
        public static IFailureLevel NonCritical { get; internal set; }
        public static IFailureLevel Warning { get; internal set; }
        public static IFailureLevel Error { get; internal set; }
        public static IFailureLevel CriticalError { get; internal set; }
    }

    public static class Registry
    {
        public static IRegistry<Identifier> IdentifierRegistry { get; } =
            Data.Registry.IdentifierRegistry.Instance;

        public static IRegistry<IDataContainer> DataContainerRegistry { get; } =
            Data.Registry.DataContainerRegistry.Instance;

        public static IRegistry<TMeta> MetaQuantsRegistry<TMeta>()
            where TMeta : IRegistrableMetaQuant
        {
            return Data.Registry.MetaQuantsRegistry<TMeta>.Instance;
        }

        public static IRegistry<T> QuantsRegistry<T>()
            where T : IRegistrableQuant
        {
            return Data.Registry.QuantsRegistry<T>.Instance;
        }
    }
}

public static class Engine_Entries
{
    public static StellarEntryPoint Core_Entry { get; internal set; }
    public static StellarEntryPoint EventSystem_Entry { get; internal set; }
    public static StellarEntryPoint ThreadingSystem_Entry { get; internal set; }
    public static StellarEntryPoint TimeSystem_Entry { get; internal set; }
    public static StellarEntryPoint FileSystem_Entry { get; internal set; }
    public static StellarEntryPoint LoggingSystem_Entry { get; internal set; }
}