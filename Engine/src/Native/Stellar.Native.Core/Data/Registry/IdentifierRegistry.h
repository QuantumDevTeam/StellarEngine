// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <optional>

#include "../Collections/ConcurrentMap/ConcurrentUnorderedMap.h"

namespace Stellar::Native::Core::Data::Registry
{
    class IdentifierRegistry final
    {
    public:
        // construction
        constexpr IdentifierRegistry() noexcept = default;
        ~IdentifierRegistry() noexcept = default;

        // copy
        IdentifierRegistry(const IdentifierRegistry&) = default;
        IdentifierRegistry(IdentifierRegistry&&) noexcept = default;
        IdentifierRegistry& operator=(const IdentifierRegistry&) = default;
        IdentifierRegistry& operator=(IdentifierRegistry&&) noexcept = default;

        // spaceship
        auto operator<=>(const IdentifierRegistry&) const noexcept = default;

        // singleton
        static IdentifierRegistry& GetInstance()
        {
            static IdentifierRegistry instance;
            return instance;
        }

    private:
        Collections::ConcurrentUnorderedIdentifierMap<Identifier> _identifiers{64};

    public:
        bool Register(const Identifier& id);
        std::optional<Identifier> Get(const Identifier& id) const;
        bool Unregister(const Identifier& id, Identifier& outValue);

        [[nodiscard]] bool Contains(const Identifier& key) const;
        [[nodiscard]] size_t size() const;

        [[nodiscard]] std::generator<const Identifier&> Identifiers() const;
    };
}
