using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.EventSystem
{
    public interface IEventContext<TEvent>
        : IContext
        where TEvent : struct, IEvent
    {
        /// <summary>
        /// событие в контексте
        /// </summary>
        TEvent Event { get; set; }
    }
}