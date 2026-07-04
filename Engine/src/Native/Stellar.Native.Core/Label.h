// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "_Mixins/Hashable.h"
#include "_Mixins/Stringable.h"
#include "_Mixins/Identifiable.h"

namespace Stellar::Native::Core
{
    struct Label final : Stringable<Label>, Hashable<Label>, Identifiable<Label>
    {
        // construction
        constexpr Label() noexcept = default;
        ~Label() noexcept = default;

        // copy
        Label(const Label&) = default;
        Label(Label&&) noexcept = default;
        Label& operator=(const Label&) = default;
        Label& operator=(Label&&) noexcept = default;

        // spaceship
        auto operator<=>(const Label&) const noexcept = default;

    private:
        // data - Identifier
        Identifier _uid = Identifier::Null();
        // data - Name
        std::string_view _name = {};

        const Identifier& GetUIDImpl() const noexcept override { return _uid; }

    public:
        // from name
        explicit constexpr Label(std::string_view name);

        // from name and id
        explicit constexpr Label(std::string_view name, Identifier id);

        // just Name getter
        constexpr const std::string_view& GetName() const { return _name; }

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
    };

#include "Label.inl"

    inline constexpr Label NullLabel = Label::Null();
    inline constexpr Label UnnamedUnboundLabel = Label::UnnamedUnbound();
}
