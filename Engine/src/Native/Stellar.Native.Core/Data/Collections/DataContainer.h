// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "ConcurrentUnordered/ConcurrentUnorderedMap.h"

namespace Stellar::Native::Core::Data::Collections
{
#ifdef IS_UNORDERED_DataContainer
#define DataContainerMAP ConcurrentUnorderedIdentifierMap
#endif
#ifdef IS_FLAT_DataContainer
#define DataContainerMAP ConcurrentFlatIdentifierMap
#endif

    template <typename TValue>
    struct DataContainer final : DataContainerMAP<TValue>
    {
        STELLAR_GENERATE_BODY(DataContainer, DataContainerMAP<TValue>)
    };

    template <typename TValue>
    [[deprecated("WritableDataContainer is just an alias, use DataContainer instead")]]
    using WritableDataContainer = DataContainer<TValue>;

    template <typename TValue>
    struct ConstantDataContainer final : DataContainerMAP<TValue>
    {
        STELLAR_GENERATE_BODY(ConstantDataContainer, DataContainerMAP<TValue>)

        bool TryAdd(const Identifier&, const TValue&) override { return false; }
        bool TryRemove(const Identifier&, TValue&) override { return false; }
    };
}
