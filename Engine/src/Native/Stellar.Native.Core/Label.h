// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Identifier.h"


namespace Stellar::Native::Core
{
    struct Label final
    {
        std::string_view Name{};
        Identifier UID{};

        // null
        constexpr Label() = default;

        // from name
        constexpr explicit Label(std::string_view name)
            : Name(name), UID(NullIdentifier)
        {
        }

        // null name and id
        constexpr Label(std::string_view name, Identifier id)
            : Name(name), UID(id)
        {
        }

        // null label
        [[nodiscard]] static constexpr Label Null() { return {}; }
        // unnamed and unbound label
        [[nodiscard]] static constexpr Label UnnamedUnbound() { return Label("Unnamed"); }

        // create new Label bounded to a random Identifier
        [[nodiscard]] static Label CreateBound(std::string_view name);
        // create new Label bounded to an Identifier
        [[nodiscard]] static Label CreateBound(std::string_view name, const Identifier& id_);

        // validate label
        [[nodiscard]] bool IsValid() const noexcept;
        // checking the binding to an identifier
        [[nodiscard]] bool IsBound() const noexcept;
        // checking the binding to a certain identifier
        [[nodiscard]] bool IsBound(const Identifier& id) const noexcept;

        STELLAR_DEFINE_CS_METHODS();

        auto operator<=>(const Label&) const = default;
    };

    inline constexpr Label NullLabel = Label::Null();

    inline constexpr Label UnnamedUnboundLabel = Label::UnnamedUnbound();

    STELLAR_DEFINE_TO_STRING(Label);
}

STELLAR_DEFINE_HASHER(Stellar::Native::Core::Label);
