using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Failures.Handlers;
using Stellar.Core.Quantization;

namespace Stellar.Core.Failures.Handlers;

public class CompositeFailureHandlerProvider()
    : RegistrableQuant<CompositeFailureHandlerProvider, HandlerProviderMeta>(new()), IFailureHandlerProvider
{
    private readonly List<IFailureHandlerProvider> _providers = new();

    public void AddProvider(IFailureHandlerProvider provider)
    {
        _providers.Add(provider);
    }

    public void RemoveProvider(IFailureHandlerProvider provider)
    {
        _providers.Remove(provider);
    }

    public IEnumerable<IFailureHandler> GetHandlers(IContext<IFailureContextData> failureContext)
    {
        return _providers.SelectMany(provider => provider.GetHandlers(failureContext));
    }
}