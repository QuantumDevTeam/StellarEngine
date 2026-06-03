#include "pch.h"
#include "EventType.h"

namespace Stellar::Native::EventSystem
{
    constexpr EventType::EventType(const Core::Label& label)
        : Label(label)
    {
    }

    uint64_t EventType::GetHashCode() const noexcept
    {
        return Label.GetHashCode();
    }

    std::string EventType::ToString() const noexcept
    {
        return std::format(
            "{}<{}>"
            "#Label({})",
            StaticClassName(), Label.Name, 
            Label.ToString());
    }
}
