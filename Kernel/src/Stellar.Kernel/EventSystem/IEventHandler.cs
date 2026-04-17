using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EventSystem
{
    public interface IEventHandler
        : IQuant
    {
        /// <summary>
        /// Ordinal event handling method. важно что дженерики помогают оптимизировать код, они же помогают типизации в хэндлере
        /// </summary>
        /// <param name="context">Event handling context (как отдельного события так и в целом контекст выполнения событий)</param>
        /// <typeparam name="TContext">type of context</typeparam>
        /// <typeparam name="TEvent">handling event type</typeparam>
        void Handle<TContext, TEvent>(ref readonly TContext context)
            where TEvent : struct, IEvent
            where TContext : IEventContext<TEvent>, allows ref struct;
        
        /// <summary>
        /// handle a batch events from queue
        /// </summary>
        /// <param name="context">execution context</param>
        /// <param name="events">events batch</param>
        /// <typeparam name="TContext">type of context</typeparam>
        /// <typeparam name="TEvent">handling event type</typeparam>
        [Obsolete("Engine method. Use Handle<TContext, TEvent> instead")]
        void HandleBatch<TContext, TEvent>(ref TContext context, ReadOnlySpan<TEvent> events)
            where TEvent : struct, IEvent
            where TContext : IEventContext<TEvent>, allows ref struct
        {
            foreach (var @event in events)
            {
                context.Event = @event;
                Handle<TContext, TEvent>(in context);
            }
        }
    }
}