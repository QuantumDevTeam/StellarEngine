// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "../ICollection.h"

#include <atomic>
#include <shared_mutex>
#include <unordered_map>


namespace Stellar::Native::Core::Data::Collections
{
    template <CHashableKey TKey, typename TValue>
    struct DoubleBufferedSnapshotMap : ICollection<TKey, TValue>
    {
        // data type
        struct DataSnapshot
        {
            std::unordered_map<TKey, TValue> map;
            uint64_t version = 0;
        };
        
    protected:

        // stored data
        std::atomic<std::shared_ptr<const DataSnapshot>> _currentSnapshot;

        // mutex for Thread-Safety writing
        mutable std::shared_mutex _writeMutex;
        // next snapshot - buffer to write
        std::shared_ptr<DataSnapshot> _writeBuffer;

        // hot elements, added as immediate = true
        mutable std::shared_mutex _hotMutex;
        std::unordered_map<TKey, std::optional<TValue>> _hotItems;

    public:
        DoubleBufferedSnapshotMap();
        STELLAR_DECONSTRUCT(DoubleBufferedSnapshotMap);
        STELLAR_DEFAULT_COPY_OPERATORS(DoubleBufferedSnapshotMap);

        STELLAR_CLASS_NAME_DEF(DoubleBufferedSnapshotMap);

        [[nodiscard]] std::shared_ptr<const DataSnapshot> GetSnapshot() const
        {
            return _currentSnapshot.load(std::memory_order_acquire);
        }

        bool TryAdd(const TKey& key, const TValue& value, bool immediate);
        [[nodiscard]] std::optional<TValue> TryGet(const TKey& key, bool immediate) const;
        [[nodiscard]] bool TryRemove(const TKey& key, TValue& outValue, bool immediate);

        [[nodiscard]] bool Contains(const TKey& key, bool immediate) const;
        [[nodiscard]] size_t size(bool immediate) const;

        [[nodiscard]] std::generator<const TKey&> Keys(bool immediate) const;
        [[nodiscard]] std::generator<const TValue&> Values(bool immediate) const;
        
        [[nodiscard]] std::shared_ptr<const DataSnapshot> GetCurrentSnapshot();
        std::shared_ptr<const DataSnapshot> TakeSnapshot();

        bool TryAdd(const TKey& key, const TValue& value) final;
        [[nodiscard]] std::optional<TValue> TryGet(const TKey& key) const final;
        [[nodiscard]] bool TryRemove(const TKey& key, TValue& outValue) final;

        [[nodiscard]] bool Contains(const TKey& key) const final;
        [[nodiscard]] size_t size() const final;

        [[nodiscard]] std::generator<const TKey&> Keys() const final;
        [[nodiscard]] std::generator<const TValue&> Values() const final;

        void Clear() final;
    };

#include "DoubleBufferedSnapshotMap.inl"

    template <typename TValue>
    using DoubleBufferedSnapshotIdentifierMap = DoubleBufferedSnapshotMap<Identifier, TValue>;
}
