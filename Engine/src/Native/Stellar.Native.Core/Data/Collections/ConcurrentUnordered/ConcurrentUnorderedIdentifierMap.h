// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "ConcurrentUnorderedMap.h"
#include "../../Identifier.h"


namespace Stellar::Native::Core::Data::Collections
{
    template <typename TValue>
    struct ConcurrentUnorderedIdentifierMap final : ConcurrentUnorderedMap<Identifier, TValue>
    {
        STELLAR_GENERATE_BODY(ConcurrentUnorderedIdentifierMap)
        
        explicit ConcurrentUnorderedIdentifierMap(size_t numSegments = DefaultNumSegments) :
            ConcurrentUnorderedMap<Identifier, TValue>(numSegments)
        {
        }
    };
}
