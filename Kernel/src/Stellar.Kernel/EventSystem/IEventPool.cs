namespace Stellar.Kernel.EventSystem
{
    public interface IEventPool
    {
        int EventCount { get; }
        bool IsEmpty { get; }
        
        void Enqueue(IEvent @event);
        bool TryDequeue(out IEvent @event);
        
        void Clear();
    }
}