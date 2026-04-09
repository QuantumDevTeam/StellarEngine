using Stellar.Kernel;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Failures.Handlers;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Collections;

namespace Stellar.Core.Failures.Handlers;

public class HandlerProviderMeta(
    (DataContainer<IFailureHandler>, Dictionary<IIdentifier, (FailureType, IFailureLevel)>)? handlers = null
) : MetaQuant(Identifier.CreateAndRegister())
{
    public readonly DataContainer<IFailureHandler> Handlers =
        handlers.HasValue ? handlers.Value.Item1 : new WritableTable<IFailureHandler>();

    public readonly Dictionary<IIdentifier, (FailureType, IFailureLevel)> Bindings =
        handlers.HasValue ? handlers.Value.Item2 : new();
}

public class FailureHandlerProvider(HandlerProviderMeta meta)
    : RegistrableQuant<FailureHandlerProvider, HandlerProviderMeta>(meta), IFailureHandlerProvider
{
    public void RegisterHandler((FailureType, IFailureLevel) binding, IFailureHandler handler)
    {
        MetaQuant.Handlers.Set(handler);
        MetaQuant.Bindings.Add(handler.UID, binding);
    }

    public IEnumerable<IFailureHandler> GetHandlers(IFailureContext<IContextData> failureContext)
    {
        if (failureContext.Failure is not { } failure) yield break;

        var handlersIdentifiers = MetaQuant.Bindings
            .Where(source =>
                failure.Type.HasFlag(source.Value.Item1) && failure.Level == source.Value.Item2)
            .Select((binding, _) => binding.Key)
            .ToArray();

        foreach (var handlersIdentifier in handlersIdentifiers)
        {
            yield return MetaQuant.Handlers[handlersIdentifier];
        }
    }
}