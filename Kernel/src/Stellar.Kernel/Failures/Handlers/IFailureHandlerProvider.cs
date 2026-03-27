using System.Collections.Generic;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures.Handlers
{
    public interface IFailureHandlerProvider : IQuantumObject
    {
        IEnumerable<IFailureHandler> GetHandlers(IContext<IFailureContextData> failure);
    }
}