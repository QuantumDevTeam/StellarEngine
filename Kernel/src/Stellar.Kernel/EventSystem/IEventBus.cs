#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EventSystem
{
    public interface IEventBus
        : IRegistrableQuant
    {
        void Subscribe(IEventHandler handler, IEventType? eventType = null, int handlerPriority = 0);
        void Unsubscribe(IEventHandler handler, IEventType? eventType = null);

        void Subscribe(IEventHandler handler, ReadOnlySpan<IEventType> eventTypes, int handlerPriority = 0);
        void Unsubscribe(IEventHandler handler, ReadOnlySpan<IEventType> eventTypes);

        void Emit<TContext, TEvent>(ref readonly TContext context)
            where TEvent : struct, IEvent
            where TContext : IEventContext<TEvent>, allows ref struct;

        Task EmitAsync<TContext, TEvent>(ref readonly TContext context)
            where TEvent : struct, IEvent
            where TContext : IEventContext<TEvent>, allows ref struct;

        // то же самое но для енумератов
        void Emit<TContext, TEvent>(ref TContext context, ReadOnlySpan<TEvent> events)
            where TEvent : struct, IEvent
            where TContext : IEventContext<TEvent>, allows ref struct;

        Task EmitAsync<TContext, TEvent>(ref TContext context, ReadOnlySpan<TEvent> events)
            where TEvent : struct, IEvent
            where TContext : IEventContext<TEvent>, allows ref struct;

        /// <summary>
        /// добавляет событие в пулл
        /// </summary>
        /// <param name="event"></param>
        void Enqueue<TEvent>(TEvent @event)
            where TEvent : struct, IEvent;

        void Enqueue<TEvent>(IEnumerable<TEvent> events)
            where TEvent : struct, IEvent;

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