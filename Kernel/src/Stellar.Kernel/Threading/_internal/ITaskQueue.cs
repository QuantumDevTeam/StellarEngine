using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Threading
{
    public interface ITaskQueue
        : IQuantumObject, IDisposable
    {
        int TaskCount { get; }
        
        void Enqueue(ITask task);
        bool TryDequeue(out ITask task);
        void Clear();
    }
}