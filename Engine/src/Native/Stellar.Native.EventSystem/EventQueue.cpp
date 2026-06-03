#include "pch.h"
#include "EventQueue.h"

namespace Stellar::Native::EventSystem
{
    EventQueue::EventQueue(const Core::Label& label)
        : _label(label)
    {
    }

    EventQueue::EventQueue(std::string_view name)
        : _label(Core::Label::CreateBound(name))
    {
    }

    void EventQueue::Enqueue(const Event& event)
    {
        _events.TryAdd(event.GetUID(), event);
    }

    std::shared_ptr<const EventQueueDataType::DataSnapshot> EventQueue::DequeueAll()
    {
        return _events.TakeSnapshot();
    }

    bool EventQueue::Contains(const Core::Identifier& key) const
    {
        return _events.Contains(key);
    }

    size_t EventQueue::size() const
    {
        return _events.size();
    }

    void EventQueue::Clear()
    {
        _events.Clear();
    }

    uint64_t EventQueue::GetHashCode() const noexcept
    {
        return _label.GetHashCode();
    }

    std::string EventQueue::ToString() const noexcept
    {
        return std::format(
            "{}<{}>"
            "#Label({})",
            StaticClassName(), std::string(_label.GetName()),
            _label.ToString());
    }
}
