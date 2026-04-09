using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.EventSystem
{
    public interface IEventContext
        : IContext
    {
        /// <summary>
        /// событие в контексте
        /// </summary>
        IEvent Event { get; }
    }
}