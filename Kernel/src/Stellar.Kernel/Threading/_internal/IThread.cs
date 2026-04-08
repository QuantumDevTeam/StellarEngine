using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Threading
{
    public interface IThread
        : IQuantumObject
    {
        bool IsAlive { get; }
        void Start();
        void Join();
    }
}