using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EventSystem;

public interface IEventQueueBase
    : IQuantumObject
{
    int EventCount { get; }
    bool IsEmpty { get; }

    void SwapBuffers();
    void DispatchTo(IEventHandler[] handlers);

    void Clear();
}

public interface IEventQueue<TEvent>
    : IRegistrableQuant, IEventQueueBase
    where TEvent : struct, IEvent
{
    void Enqueue(TEvent @event);
    bool TryDequeue(out TEvent @event);
}