#nullable enable

using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Label;

namespace Stellar.Kernel.EventSystem;

// TODO: ADD DOCS (IMPORTANT)
public interface IEventPostContext<TEvent>
    : IContext
    where TEvent : struct, IEvent
{
    ILabel? QueueToPost { get; set; }
    TEvent Event { get; set; }
}