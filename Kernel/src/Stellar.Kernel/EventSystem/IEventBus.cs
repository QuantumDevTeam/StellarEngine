#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EventSystem;

// TODO: ADD DOCS (IMPORTANT)
public interface IEventBus
    : IRegistrableQuant
{
    void AddQueue(IEventQueueBase queue);

    void Register(IEventHandler handler);
    void Register(IEventType eventType);

    void Subscribe(IEventHandler handler, ILabel queue, IEventType? eventType = null, float handlerPriority = 0);
    void Unsubscribe(IEventHandler handler, ILabel queue, IEventType? eventType = null);

    // то же самое но для енумератов
    void Emit<TContext, TEvent>(ref TContext context, ReadOnlySpan<TEvent> events)
        where TEvent : struct, IEvent
        where TContext : IEventPostContext<TEvent>, allows ref struct;

    Task EmitAsync<TContext, TEvent>(ref TContext context, ReadOnlySpan<TEvent> events,
        CancellationToken cancellationToken = default)
        where TEvent : struct, IEvent
        where TContext : IEventPostContext<TEvent>, allows ref struct;

    // добавляет событие в очередь
    void Enqueue<TContext, TEvent>(ref TContext context, ReadOnlySpan<TEvent> events)
        where TEvent : struct, IEvent
        where TContext : IEventPostContext<TEvent>, allows ref struct;

    // Обработка
    void Process(ILabel? queue);
    Task ProcessAsync(ILabel? queue, CancellationToken cancellationToken = default);

    // простая очистка всего
    void Clear();
}