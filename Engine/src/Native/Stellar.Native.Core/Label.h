// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Identifier.h"

namespace Stellar::Native::Core
{
    struct Label final
    {
        constexpr Label() noexcept = default;
        ~Label() noexcept = default;
        STELLAR_DEFAULT_COPY_OPERATORS(Label);

    private:
        Identifier _uid = Identifier::Null();

        std::string_view _name = {};

    public:
        // from name
        explicit constexpr Label(std::string_view name);

        // from name and id
        explicit constexpr Label(std::string_view name, Identifier id);

        STELLAR_CLASS_NAME_DEF(Label)

        ConstexprGetter(const Identifier&, UID) { return _uid; }
        ConstexprGetter(const std::string_view&, Name) { return _name; }

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

        [[nodiscard]] STELLAR_TO_STRING();
        [[nodiscard]] STELLAR_HASHCODE();

        STELLAR_SPACESHIP(Label);
    };
    
#include "Label.inl"

    constexpr Label NullLabel;

    constexpr Label UnnamedUnboundLabel;

    STELLAR_GENERATE_TO_STRING(Label)
}

STELLAR_GENERATE_HASHER(Stellar::Native::Core::Label)
