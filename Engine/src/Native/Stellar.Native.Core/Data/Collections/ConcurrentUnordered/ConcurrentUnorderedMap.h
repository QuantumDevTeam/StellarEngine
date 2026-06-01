// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "../IConcurrentMap.h"

#include <shared_mutex>
#include <unordered_map>


namespace Stellar::Native::Core::Data::Collections
{
    inline constexpr size_t DefaultNumSegments = 16;

    template <HashableKey TKey, typename TValue>
    struct ConcurrentUnorderedMap : IConcurrentMap<TKey, TValue>
    {
    protected:
        // data type
        struct Segment
        {
            mutable std::shared_mutex mutex;
            std::unordered_map<TKey, TValue> data;

            [[nodiscard]] std::size_t size() const;
        };

        // stored data
        std::vector<std::unique_ptr<Segment>> Segments;

        // Gets Segment by stored data Key
        [[nodiscard]] std::size_t GetSegmentIndex(const TKey& key) const noexcept;

    public:
        explicit ConcurrentUnorderedMap(size_t numSegments = DefaultNumSegments);
        STELLAR_DECONSTRUCT(ConcurrentUnorderedMap);
        STELLAR_DEFAULT_COPY_OPERATORS(ConcurrentUnorderedMap);
        
        STELLAR_CLASS_NAME_DEF(ConcurrentUnorderedMap);

        bool TryAdd(const TKey& key, const TValue& value) final;
        [[nodiscard]] std::optional<TValue> TryGet(const TKey& key) const final;
        bool TryRemove(const TKey& key, TValue& outValue) final;

        [[nodiscard]] bool Contains(const TKey& key) const final;
        [[nodiscard]] size_t size() const final;

        [[nodiscard]] std::vector<TKey> Keys() const final;
        [[nodiscard]] std::vector<TValue> Values() const final;

        void Clear() final;
    };

#include "ConcurrentUnorderedMap.inl"
}
