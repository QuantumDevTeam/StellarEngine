// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Identifier.h"

namespace Stellar::Native::Core
{
    struct Label
    {
        std::string Name;
        Identifier UID;

        // null
        Label() = default;

        // from name
        explicit Label(std::string_view name)
            : Name(name), UID(Identifier::Null())
        {
        }

        // null name and id
        Label(std::string_view name, Identifier id)
            : Name(name), UID(id)
        {
        }

        static Label CreateBound(std::string_view name);

        [[nodiscard]] bool IsValid() const noexcept
        {
            return !Name.empty();
        }

        [[nodiscard]] bool IsBound() const noexcept
        {
            return !UID.IsNull();
        }

        [[nodiscard]] bool IsBound(const Identifier& id) const noexcept
        {
            return UID == id;
        }

        auto operator<=>(const Label&) const = default;
    };

    inline constexpr Label NullLabel = Label();
}
