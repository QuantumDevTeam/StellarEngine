// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

#include "../Identifier.h"

namespace Stellar::Native::Core
{
    template <typename T>
    concept CIdentifiable = requires(const T& obj)
    {
        { obj.GetUID() } -> std::convertible_to<const Identifier&>;
    };

    template <typename Derived>
    struct Identifiable
    {
        // TODO: It's really necessary?
    private:
        // construction
        Identifiable() = default;
        ~Identifiable() noexcept = default;

        // copy
        Identifiable(const Identifiable&) = default;
        Identifiable(Identifiable&&) noexcept = default;
        Identifiable& operator=(const Identifiable&) = default;
        Identifiable& operator=(Identifiable&&) noexcept = default;

        // spaceship
        auto operator<=>(const Identifiable&) const noexcept = default;

    protected:
        // TODO: VIRTUAL
        [[nodiscard]] virtual const Identifier& GetUIDImpl() const noexcept
        {
            return static_cast<const Derived&>(*this)._uid;
        }

    public:
        // TODO: VIRTUAL
        [[nodiscard]] const Identifier& GetUID() const noexcept
        {
            // static polymorph
            return static_cast<const Derived&>(*this).GetUIDImpl();
        }

        friend Derived;
    };
}

STELLAR_CLANG_IGNORE_END()
