using System.Collections.Generic;
using Stellar.Kernel.Data.Collections;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EventSystem;

// TODO: ADD DOCS (IMPORTANT)
public interface IEventQueueBase
    : IRegistrableQuant
{
    int Count { get; }
    bool IsEmpty { get; }

    void Prepare();
    void DispatchTo(IEnumerable<IEventHandler> handlers);

    void Clear();
}

// TODO: ADD DOCS (IMPORTANT)
public interface IEventQueue<TEvent>
    : IQueueBase<TEvent>, IEventQueueBase, ILabeled
    where TEvent : struct, IEvent
{
    new void Enqueue(TEvent @event);
    new bool TryDequeue(out TEvent @event);
}