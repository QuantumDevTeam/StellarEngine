namespace Stellar.Kernel.EventSystem
{
    public interface IEventPool<TEvent>
        where TEvent : IEvent, allows ref struct
    {
        int EventCount { get; }
        bool IsEmpty { get; }

        void Enqueue(TEvent @event);
        bool TryDequeue(out TEvent @event);

        void Clear();
    }
}