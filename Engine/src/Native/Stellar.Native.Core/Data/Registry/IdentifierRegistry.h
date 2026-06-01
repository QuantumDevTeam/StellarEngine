// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "../Collections/ConcurrentUnordered/ConcurrentUnorderedIdentifierMap.h"


namespace Stellar::Native::Core::Data::Registry
{
    class IdentifierRegistry
    {
        STELLAR_GENERATE_SINGLETON(IdentifierRegistry)

    private:
        Collections::ConcurrentUnorderedIdentifierMap<Identifier> _identifiers{64};

    public:
        bool Register(const Identifier& id);
        std::optional<Identifier> Get(const Identifier& id) const;
        bool Unregister(const Identifier& id, Identifier& outValue);

        [[nodiscard]] bool Contains(const Identifier& key) const;
        [[nodiscard]] size_t size() const;

        [[nodiscard]] std::vector<Identifier> Identifiers() const;
    };
}
