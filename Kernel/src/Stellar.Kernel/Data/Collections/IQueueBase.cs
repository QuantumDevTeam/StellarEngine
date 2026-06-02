using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Collections
{
    // TODO: ADD DOCS (IMPORTANT)
    public interface IQueueBase<T>
        : IRegistrableQuant
        where T : struct
    {
        int Count { get; }
        bool IsEmpty { get; }

        void Enqueue(T @event);
        bool TryDequeue(out T @event);

        void Clear();
    }
}