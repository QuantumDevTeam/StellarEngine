using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures
{
    public interface IDispatcher
    {
        void Dispatch(IContext<IFailureContextData> failure);
    }
}