namespace Stellar.Kernel.EventSystem
{
    public interface IEventQueue<TEvent>
        where TEvent : IEvent, allows ref struct
    {
        int EventCount { get; }
        bool IsEmpty { get; }

        void Enqueue(TEvent @event);
        bool TryDequeue(out TEvent @event);

        void Clear();
    }
}