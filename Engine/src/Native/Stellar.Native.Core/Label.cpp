#include "pch.h"
#include "Label.h"

namespace Stellar::Native::Core
{
    Label Label::CreateBound(std::string_view name)
    {
        return {name, Identifier::Create()};
    }

    Label Label::CreateBound(std::string_view name, const Identifier& id_)
    {
        return {name, id_};
    }

    bool Label::IsValid() const noexcept
    {
        return !Name.empty();
    }

    bool Label::IsBound() const noexcept
    {
        return !UID.IsNull();
    }

    bool Label::IsBound(const Identifier& id) const noexcept
    {
        return UID == id;
    }

    std::string Label::ToString() const noexcept
    {
        return std::format(
            "{}"
            "#UID({})"
            "#Name({})",
            StaticClassName(),
            UID.ToString(), Name);
    }

    uint64_t Label::GetHashCode() const noexcept
    {
        return UID.GetHashCode();
    }
}
