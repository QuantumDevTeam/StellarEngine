#include "pch.h"
#include "Event.h"

namespace Stellar::Native::EventSystem
{
    constexpr Event::Event(const EventType& type, uint64_t timestamp)
        : _type(type), _timestamp(timestamp)
    {
    }

    uint64_t Event::GetHashCode() const noexcept
    {
        return _uid.GetHashCode();
    }

    std::string Event::ToString() const noexcept
    {
        return std::format(
            "{}<{}>"
            "#Label({})"
            "#Timestamp({})",
            StaticClassName(), _type.GetLabel().Name, 
            _type.GetLabel(), _timestamp);
    }
}
