using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Threading
{
    public interface IThreadPool
        : IQuantumObject, IDisposable
    {
        ITaskQueue TaskQueue { get; }
        int ThreadCount { get; }
 
        void Start(int threadCount);
        void Resize(int newThreadCount, bool waitForTasks = true);
        void Stop(bool waitForTasks = true);
    }
}