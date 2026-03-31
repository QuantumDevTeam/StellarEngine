using Stellar.Kernel;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Failures.Handlers;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Collections;

namespace Stellar.Core.Failures.Handlers;

public class HandlerProviderMeta(
    (DataContainer<IFailureHandler>, Dictionary<IIdentifier, (FailureType, IFailureLevel)>)? handlers = null,
    IIdentifier? identifier = null
) : MetaQuant(identifier)
{
    public readonly DataContainer<IFailureHandler> Handlers =
        handlers.HasValue ? handlers.Value.Item1 : new WritableDataContainer();

    public readonly Dictionary<IIdentifier, (FailureType, IFailureLevel)> Bindings =
        handlers.HasValue ? handlers.Value.Item2 : new();
}

public class HandlerProvider(HandlerProviderMeta meta)
    : RegistrableQuant<HandlerProvider, HandlerProviderMeta>(meta), IFailureHandlerProvider
{
    public void RegisterHandler((FailureType, IFailureLevel) binding, IFailureHandler handler)
    {
        MetaQuant.Handlers.Add(handler);
        MetaQuant.Bindings.Add(handler.UID, binding);
    }

    public IEnumerable<IFailureHandler> GetHandlers(IContext<IFailureContextData> failureContext)
    {
        if (failureContext.Data?.Failure is not { } failure)
            return new List<IFailureHandler>();

        var bindings = (failure.Type, failure.Level);

        throw new NotImplementedException();
    }
}