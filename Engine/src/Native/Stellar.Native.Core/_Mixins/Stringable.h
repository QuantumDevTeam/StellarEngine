// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

namespace Stellar::Native::Core
{
    template <typename T>
    concept CStringable = requires(const T& obj)
    {
        { obj.ToString() } -> std::convertible_to<std::string>;
    };
    
    template <CStringable T>
    [[nodiscard]] std::string to_string(const T& obj) noexcept
    {
        return obj.ToString();
    }

    template <typename Derived>
    struct Stringable
    {
        // TODO: It's really necessary?
    private:
        // construction
        Stringable() = default;
        ~Stringable() noexcept = default;

        // copy
        Stringable(const Stringable&) = default;
        Stringable(Stringable&&) noexcept = default;
        Stringable& operator=(const Stringable&) = default;
        Stringable& operator=(Stringable&&) noexcept = default;

        // spaceship
        auto operator<=>(const Stringable&) const noexcept = default;

    protected:
        // TODO: VIRTUAL
        [[nodiscard]] virtual std::string ToStringImpl() const noexcept
        {
            return std::format(
                "{}#UID({})",
                typeid(Derived).name(),
                static_cast<const Derived&>(*this).GetUID().ToString()
            );
        }

    public:
        // TODO: VIRTUAL
        [[nodiscard]] virtual std::string ToString() const noexcept
        {
            // static polymorph
            return static_cast<const Derived&>(*this).ToStringImpl();
        }

        friend Derived;
    };
}

STELLAR_CLANG_IGNORE_END()
