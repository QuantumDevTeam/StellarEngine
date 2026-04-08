using System.Threading;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Threading
{
    public interface ITaskContext<out T>
        : IContext<T>
        where T : ITaskData
    {
        ITask Task { get; }
        CancellationToken CancellationToken { get; }
    }
}