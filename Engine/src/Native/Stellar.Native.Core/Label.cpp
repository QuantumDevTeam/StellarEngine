#include "pch.h"
#include "Label.h"

namespace Stellar::Native::Core
{
    Label Label::CreateBound(std::string_view name)
    {
        return {name, Identifier::Create()};
    }
}
