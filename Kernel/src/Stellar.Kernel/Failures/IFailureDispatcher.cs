using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures
{
    public interface IFailureDispatcher
        : IQuant
    {
        bool Dispatch(IContext<IFailureContextData> failureContext);
    }
}