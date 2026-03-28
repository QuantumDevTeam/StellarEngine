using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures.Handlers
{
    public interface IFailureHandler : IRegistrableQuant
    {
        bool Handle(IFailure failure);
    }
}