#include "pch.h"
#include "EventType.h"

namespace Stellar::Native::EventSystem
{
    constexpr EventType::EventType(const Core::Label& label)
        : _label(label)
    {
    }

    uint64_t EventType::GetHashCode() const noexcept
    {
        return _label.GetHashCode();
    }

    std::string EventType::ToString() const noexcept
    {
        return std::format(
            "{}<{}>"
            "#Label({})",
            StaticClassName(), std::string(_label.GetName()), 
            _label.ToString());
    }
}
