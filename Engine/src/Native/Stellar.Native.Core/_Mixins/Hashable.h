// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

namespace Stellar::Native::Core
{
    template <typename T>
    concept CHashable = requires(const T& obj)
    {
        { obj.GetHashCode() } -> std::convertible_to<uint64_t>;
    };

    template <typename Derived>
    struct Hashable
    {
        // TODO: It's really necessary?
    private:
        // construction
        Hashable() = default;
        ~Hashable() noexcept = default;

        // copy
        Hashable(const Hashable&) = default;
        Hashable(Hashable&&) noexcept = default;
        Hashable& operator=(const Hashable&) = default;
        Hashable& operator=(Hashable&&) noexcept = default;

        // spaceship
        auto operator<=>(const Hashable&) const noexcept = default;

    protected:
        // TODO: VIRTUAL
        [[nodiscard]] virtual std::string GetHashCodeImpl() const noexcept
        {
            return static_cast<const Derived&>(*this).GetUID().GetHashCode();
        }

    public:
        // TODO: VIRTUAL
        [[nodiscard]] virtual uint64_t GetHashCode() const noexcept
        {
            return static_cast<const Derived&>(*this).GetHashCodeImpl();
        }

        friend Derived;
    };
}

template <Stellar::Native::Core::CHashable T>
struct std::hash<T> // NOLINT(cert-dcl58-cpp)
{
    size_t operator()(const T& obj) const noexcept
    {
        return static_cast<size_t>(obj.GetHashCode());
    }
};

STELLAR_CLANG_IGNORE_END()
