using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    public interface IFailureLevel : IRegistrableQuantumObject
    {
        bool IsEnabled { get; set; }
        bool IsLoggable { get; }
        bool IsStopExecute { get; }
        bool IsCritical { get; }
        bool ShouldTerminate { get; }
    }
}