using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Threading
{
    public interface ITaskScheduler
        : IQuantumObject, IDisposable
    {
        bool IsRinning { get; }

        void Schedule(ITask task);
        void Schedule(IEnumerable<ITask> tasks);

        void Run();
        Task RunAsync();

        void Pause(double time = double.PositiveInfinity);
        ITask SetImportant(IIdentifier taskIdentifier, bool important);
        void WaitForCompletion(double maxTime = Double.PositiveInfinity);
        void Reset();
    }
}