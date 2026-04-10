using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.EventSystem
{
    public interface IEventContext<out TEvent>
        : IContext
        where TEvent : IEvent, allows ref struct
    {
        /// <summary>
        /// событие в контексте
        /// </summary>
        TEvent Event { get; }
    }
}