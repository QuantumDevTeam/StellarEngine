using System.Collections.Generic;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures.Handlers
{
    public interface IFailureHandlerProvider : IRegistrableQuant
    {
        IEnumerable<IFailureHandler> GetHandlers(IContext<IFailureContextData> failureContext);
    }
}