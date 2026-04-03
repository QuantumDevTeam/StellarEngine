using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Logging;

namespace Stellar.Kernel.Failures.Handlers
{
    public interface IFailureHandler : IRegistrableQuant
    {
#if NETSTANDARD2_0
        ILogger Logger { get; }
#else
#nullable enable
        ILogger? Logger { get; }
#endif

        bool Handle(IContext<IFailureContextData> context);
    }
}