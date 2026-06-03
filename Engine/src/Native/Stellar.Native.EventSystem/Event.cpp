#include "pch.h"
#include "Event.h"

namespace Stellar::Native::EventSystem
{
    constexpr Event::Event(const EventType& type, uint64_t timestamp)
        : _type(type), _timestamp(timestamp)
    {
    }

    constexpr uint64_t Event::GetHashCode() const noexcept
    {
        return _uid.GetHashCode();
    }

    constexpr std::string Event::ToString() const noexcept
    {
        return std::format(
            "{}<{}>"
            "#Label({})"
            "#Timestamp({})",
            StaticClassName(), _type.GetLabel().GetName(), 
            _type.GetLabel().ToString(), _timestamp);
    }
}
