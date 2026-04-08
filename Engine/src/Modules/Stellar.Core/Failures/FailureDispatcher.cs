using Stellar.Kernel;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Collections;
using Stellar.Core.Failures.Handlers;
using Stellar.Kernel.Failures.Handlers;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Failures;

public interface IFailureDispatcherMeta
    : IMetaQuant
{
    public DataContainer<IFailureLevel> FailureLevels { get; }
    public IFailureHandlerProvider FailureHandlerProvider { get; }
}

public class FailureDispatcherMeta(
    DataContainer<IFailureLevel>? failureLevels = null,
    IFailureHandlerProvider? failureHandlerProvider = null,
    IIdentifier? identifier = null
) : MetaQuant(identifier), IFailureDispatcherMeta
{
    public DataContainer<IFailureLevel> FailureLevels { get; } =
        failureLevels ?? new WritableTable<IFailureLevel>();

    public IFailureHandlerProvider FailureHandlerProvider { get; } =
        failureHandlerProvider ?? new FailureHandlerProvider(new HandlerProviderMeta());
}

public class FailureDispatcher(
    IFailureDispatcherMeta meta
) : Quant<IFailureDispatcherMeta>(meta), IFailureDispatcher
{
    // TODO: Init from EntryPoint
    // public static readonly FailureDispatcherMeta DefaultMeta = new();
    // public static readonly FailureDispatcher Default = new(DefaultMeta);
    //
    // public static readonly Lazy<Dictionary<string, IFailureDispatcher>> Dispatchers =
    //     new(() => new Dictionary<string, IFailureDispatcher>(
    //         [new KeyValuePair<string, IFailureDispatcher>("Default", Default)]
    //     ));

    public bool Dispatch(IContext<IFailureContextData> failureContext)
    {
        var handlers = MetaQuant.FailureHandlerProvider.GetHandlers(failureContext);
        return handlers.Aggregate(true, (current, handler) => current & handler.Handle(failureContext));
    }
}