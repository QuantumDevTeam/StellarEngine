using System;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.EventSystem;

// TODO: ADD DOCS (IMPORTANT)
public interface IEvent
    : IContextData
{
    /// <summary>
    /// Event type
    /// </summary>
    IEventType EventType { get; }
        
    /// <summary>
    /// tyme of event creation
    /// </summary>
    DateTime TimeStamp { get; }
        
    /// <summary>
    /// mark event as reusable
    /// </summary>
    bool CanBeReused { get; }

    /// <summary>
    /// Property getter that called during processing and tills the bus the event needs to be passed to handler 
    /// </summary>
    bool ShouldProcessNow { get; }
}