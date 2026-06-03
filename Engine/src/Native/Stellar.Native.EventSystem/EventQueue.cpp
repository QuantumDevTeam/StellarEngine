#include "pch.h"
#include "EventQueue.h"

namespace Stellar::Native::EventSystem
{
    EventQueue::EventQueue(const Core::Label& label)
        : _label(label)
    {
    }

    EventQueue::EventQueue(std::string_view name)
        : _label(Core::Label(name))
    {
    }

    void EventQueue::Enqueue(const Event& event)
    {
        _events.TryAdd(event.GetUID(), event);
    }

    std::vector<Event> EventQueue::DequeueAll() const
    {
        std::vector<Event> result;
        for (auto& event : _events.Values())
        {
            result.emplace_back(std::move(event));
        }
        return result;
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
