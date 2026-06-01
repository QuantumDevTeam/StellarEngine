// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

#include <vector>
#include <concepts>


namespace Stellar::Native::Core::Data::Collections
{
    template <typename Key>
    concept HashableKey = requires(Key k)
    {
        { std::hash<Key>{}(k) } -> std::convertible_to<std::size_t>;
        { k == k } -> std::convertible_to<bool>;
    };

    template <typename TMember, typename TKey, typename TValue>
    concept ConcurrentMap = requires(TMember member, const TMember constMember,
                                     const TKey& key, const TValue& value, TValue& outValue)
    {
        { member.TryAdd(key, value) } -> std::same_as<bool>;
        { constMember.TryGet(key) } -> std::same_as<std::optional<TValue>>;
        { member.TryRemove(key, outValue) } -> std::same_as<bool>;

        { constMember.Contains(key) } -> std::same_as<bool>;
        { constMember.size() } -> std::same_as<size_t>;

        { constMember.Keys() } -> std::same_as<std::vector<TKey>>;
        { constMember.Values() } -> std::same_as<std::vector<TValue>>;

        { member.Clear() } -> std::same_as<void>;
    };

    template <HashableKey TKey, typename TValue>
    class IConcurrentMap
    {
        STELLAR_GENERATE_INTERFACE(IConcurrentMap);
        
        virtual bool TryAdd(const TKey& key, const TValue& value) = 0;
        [[nodiscard]] virtual std::optional<TValue> TryGet(const TKey& key) const = 0;
        virtual bool TryRemove(const TKey& key, TValue& outValue) = 0;

        [[nodiscard]] virtual bool Contains(const TKey& key) const = 0;
        [[nodiscard]] virtual size_t size() const = 0;

        [[nodiscard]] virtual std::vector<TKey> Keys() const = 0;
        [[nodiscard]] virtual std::vector<TValue> Values() const = 0;

        virtual void Clear() = 0;
    };
}

STELLAR_CLANG_IGNORE_END()
