using System.Collections.Generic;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Threading
{
    public interface ITask
        : IIdentifiableQuantumObject
    {
        int Priority { get; }
        IReadOnlyList<IIdentifier> Dependencies { get; }

        void Execute(ITaskContext<ITaskData> context);
    }
}