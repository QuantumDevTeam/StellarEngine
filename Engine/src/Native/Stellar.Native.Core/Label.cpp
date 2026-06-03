#include "pch.h"
#include "Label.h"

namespace Stellar::Native::Core
{
    Label Label::CreateBound(std::string_view name)
    {
        return Label{name, Identifier::Create()};
    }

    Label Label::CreateBound(std::string_view name, const Identifier& id_)
    {
        return Label{name, id_};
    }

    bool Label::IsValid() const noexcept
    {
        return !_name.empty();
    }

    bool Label::IsBound() const noexcept
    {
        return !_uid.IsNull();
    }

    bool Label::IsBound(const Identifier& id) const noexcept
    {
        return _uid == id;
    }

    std::string Label::ToString() const noexcept
    {
        return std::format(
            "{}"
            "#UID({})"
            "#Name({})",
            StaticClassName(),
            _uid.ToString(), _name);
    }

    uint64_t Label::GetHashCode() const noexcept
    {
        return _uid.GetHashCode();
    }
}
