#include "pch.h"
#include "EventBus.h"

#include <algorithm>
#include <ranges>


namespace Stellar::Native::EventSystem
{
    EventBus::EventBus(Core::Identifier uid)
        : _uid(uid)
    {
    }

    void EventBus::AddQueue(EventQueue* queue)
    {
        _queues.TryAdd(queue->GetUID(), std::move(queue));
    }

    EventQueue* EventBus::GetQueue(const Core::Identifier& id) const
    {
        if (auto ret = _queues.TryGet(id))
            return ret.value();
        return nullptr;
    }

    EventQueue* EventBus::RemoveQueue(const Core::Identifier& id)
    {
        EventQueue* ret = nullptr;
        _queues.TryRemove(id, ret);
        return ret;
    }

    void EventBus::Subscribe(const EventType& type, std::function<void(const Event&)> callback, float priority)
    {
        auto handlers = _subscriptions.TryGet(type.GetUID());
        if (!handlers)
        {
            _subscriptions.TryAdd(type.GetUID(), {});
            handlers = _subscriptions.TryGet(type.GetUID());
        }
        handlers->push_back({priority, std::move(callback)});
        std::sort(handlers->begin(), handlers->end()); // TODO: NOLINT(modernize-use-ranges)
    }

    void EventBus::Unsubscribe(const EventType& type)
    {
        std::vector<Handler> _;
        _subscriptions.TryRemove(type.GetUID(), _);
    }

    void EventBus::Emit(const Event& event) const
    {
        if (auto handlers = _subscriptions.TryGet(event.GetEventType().GetUID()))
        {
            for (const auto& handler : *handlers)
            {
                if (handler.Callback) handler.Callback(event);
            }
        }
    }

    void EventBus::Enqueue(const Core::Identifier& queueId, const Event& event) const
    {
        if (auto queue = _queues.TryGet(queueId))
        {
            (*queue)->Enqueue(event);
        }
    }

    void EventBus::ProcessQueue(const Core::Identifier& queueId) const
    {
        if (auto queue = _queues.TryGet(queueId))
        {
            auto events = (*queue)->DequeueAll()->map;
            for (const auto& event : events | std::views::values)
            {
                Emit(event);
            }
        }
    }

    void EventBus::Clear(bool withQueue)
    {
        if (withQueue)
        {
            for (auto queue : _queues.Values())
            {
                queue->Clear();
            }
        }
        _queues.Clear();
        _subscriptions.Clear();
    }

    uint64_t EventBus::GetHashCode() const noexcept
    {
        return _uid.GetHashCode();
    }

    std::string EventBus::ToString() const noexcept
    {
        return std::format(
            "{}"
            "#UID({})",
            StaticClassName(),
            _uid.ToString());
    }
}
