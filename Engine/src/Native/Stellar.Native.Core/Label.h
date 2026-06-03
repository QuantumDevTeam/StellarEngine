// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Identifier.h"

namespace Stellar::Native::Core
{
    struct Label final
    {
        STELLAR_GENERATE_BODY_PARTIAL(Label, constexpr,, noexcept)

    private:
        Identifier _uid{};
        std::string_view _name{};

    public:
        // from name
        explicit constexpr Label(std::string_view name)
            : _name(name), _uid(NullIdentifier)
        {
        }

        // from name and id
        explicit constexpr Label(std::string_view name, Identifier id)
            : _name(name), _uid(id)
        {
        }

        ConstexprGetter(Identifier, UID) { return _uid; }
        ConstexprGetter(std::string_view, Name) { return _name; }

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

        STELLAR_DEFAULTS(Label);
    };

    inline constexpr Label NullLabel = Label::Null();

    inline constexpr Label UnnamedUnboundLabel = Label::UnnamedUnbound();
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::Core, Label);
