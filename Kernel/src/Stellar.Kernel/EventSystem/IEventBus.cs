using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stellar.Kernel.EventSystem
{
    public interface IEventBus
    {
#if NETSTANDARD2_0
        /// <summary>
        /// подписка на определённый тип
        /// </summary>
        /// <param name="handler">обработчик который подписывается</param>
        /// <param name="eventType">тип события на который он подписывается, null если подписка на все типы</param>
        /// <param name="handlerPriority">приоритет обработчика, <0 - движковые, >0 игровые обработчики</param>
        void Subscribe(IEventHandler handler, IEventType eventType = null, int handlerPriority = 0);
        /// <summary>
        /// отписка от типа события
        /// </summary>
        /// <param name="handler">обработчик который отписывается</param>
        /// <param name="eventType">тип события для отписки</param>
        void Unsubscribe(IEventHandler handler, IEventType eventType = null);
#else
#nullable enable
        void Subscribe(IEventHandler handler, IEventType? eventType = null, int handlerPriority = 0);
        void Unsubscribe(IEventHandler handler, IEventType? eventType = null);
#endif

        // всё то-же самое но только для енумератов
        
        void Subscribe(IEventHandler handler, IEnumerable<IEventType> eventTypes, int handlerPriority = 0);
        void Unsubscribe(IEventHandler handler, IEnumerable<IEventType> eventTypes);
        
        /// <summary>
        /// немедленно испускает событие, важно в некоторых случаях
        /// </summary>
        /// <param name="event">событие которое бросается в обработчики</param>
        void Emit(IEvent @event);
        Task EmitAsync(IEvent @event, CancellationToken cancellationToken = default);

        // то же самое но для енумератов
        void Emit(IEnumerable<IEvent> events);
        Task EmitAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken = default);

        /// <summary>
        /// добавляет событие в пулл
        /// </summary>
        /// <param name="event"></param>
        void Enqueue(IEvent @event);
        void Enqueue(IEnumerable<IEvent> events);
        
        /// <summary>
        /// Обработка всего пула в момент когда геймлупа подойдёт к этому этапу
        /// </summary>
        void ProcessAll();
        Task ProcessAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// тупо очистка пула
        /// </summary>
        void Clear();
    }
}