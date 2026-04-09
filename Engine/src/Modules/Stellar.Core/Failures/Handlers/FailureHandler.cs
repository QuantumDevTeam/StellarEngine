using Stellar.Kernel.Data.Context;
using Stellar.Kernel.EntryPoint;
using Stellar.Kernel.Failures;
using Stellar.Kernel.Failures.Handlers;
using Stellar.Kernel.Logging;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Context;
using Stellar.Core.Data.Context.Defaults;

namespace Stellar.Core.Failures.Handlers;

public class FailureHandler()
    : RegistrableQuant<FailureHandler, MetaQuant>(new MetaQuant(Identifier.CreateAndRegister())), IFailureHandler
{
    public ILogger? Logger { get; set; }

    public bool Handle(IFailureContext context)
    {
        if (context.Failure is not { } failure) return true;
        if (!failure.Level.IsEnabled) return true;

        if (failure.Level.IsLoggable)
            Logger?.Log(LogLevel.Exception, failure.Message);
        if (failure.Level.IsStopExecute)
            throw new NotImplementedException("implement S.R.I");

        // Stellar.Runtime.Entry.App.RequestStop(
        //     new Context<StopContextData>(
        //         context.Sender, new StopContextData(StopReason.CriticalError, failure)
        //     )
        // );

        if (failure.Level is { IsCritical: true } or { IsStopExecute: true }) return false;

        return true;
    }
}