// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <shared_mutex>
#include <unordered_map>

#include "../../../Identifier.h"
#include "../ICollection.h"

namespace Stellar::Native::Core::Data::Collections
{
    template <CHashableKey TKey, typename TValue>
    struct ConcurrentUnorderedMap : ICollection<TKey, TValue>
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
        STELLAR_DECONSTRUCT(ConcurrentUnorderedMap, override);
        STELLAR_DEFAULT_COPY_OPERATORS(ConcurrentUnorderedMap);

        bool TryAdd(const TKey& key, const TValue& value) override;
        [[nodiscard]] std::optional<TValue> TryGet(const TKey& key) const final;
        bool TryRemove(const TKey& key, TValue& outValue) override;

        [[nodiscard]] bool Contains(const TKey& key) const final;
        [[nodiscard]] size_t size() const final;

        [[nodiscard]] std::generator<const TKey&> Keys() const final;
        [[nodiscard]] std::generator<const TValue&> Values() const final;

        template <typename TReturn, CIterationInvoking<TReturn, TKey, TValue> F>
        std::generator<TReturn> ForEachReadOnly(F action) const;

        template <CPredicate<TKey, TValue> Pred>
        std::generator<std::pair<TKey, TValue>> ForEachPredicateRemove(Pred pred);

        void Clear() final;

        STELLAR_DEFAULTS(ConcurrentUnorderedMap);
    };

#define IS_UNORDERED
#include "ConcurrentMap.inl"
#undef IS_UNORDERED

    template <typename TValue>
    using ConcurrentUnorderedIdentifierMap = ConcurrentUnorderedMap<Identifier, TValue>;
}
