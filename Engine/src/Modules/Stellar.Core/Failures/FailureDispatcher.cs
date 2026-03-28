using Stellar.Kernel;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Failures;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Collections;

namespace Stellar.Core.Failures;

public class FailureDispatcherMeta(
    DataContainer<IFailureLevel>? failureLevels = null,
    IIdentifier? identifier = null
) : MetaQuant(identifier)
{
    public DataContainer<IFailureLevel> FailureLevels { get; private set; } =
        failureLevels ?? new WritableDataContainer();
}

public class FailureDispatcher(
    FailureDispatcherMeta meta
) : Quant<FailureDispatcherMeta>(meta), IFailureDispatcher
{
    private static readonly Lazy<FailureDispatcher> Dispatcher =
        new(() => new FailureDispatcher(new FailureDispatcherMeta())); // TODO: initialize Dispatcher

    public static FailureDispatcher Instance => Dispatcher.Value;

    public void Dispatch(IContext<IFailureContextData> failureContext)
    {
        // TODO: filter context for providers

        // if (failureContext.Data?.Failure is not { } failure)
        // return;
        // QuantsRegistry<HandlerProvider>.Instance.Values
    }
}