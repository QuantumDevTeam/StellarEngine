using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures.Handlers
{
    public interface IFailureHandler : IQuantumObject
    {
        bool Handle(IFailure failure);
    }
}