// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

#include <concepts>
#include <generator>
#include <functional>

namespace Stellar::Native::Core::Data::Collections
{
    template <typename Key>
    concept CHashableKey =
        requires(Key k)
        {
            { std::hash<Key>{}(k) } -> std::convertible_to<std::size_t>;
            { k == k } -> std::convertible_to<bool>;
        };

    template <typename Key>
    concept CSortableKey =
        requires(Key k)
        {
            { k < k } -> std::convertible_to<bool>;
            { k == k } -> std::convertible_to<bool>;
        };

    template <typename TMember, typename TKey, typename TValue>
    concept CCollection =
        requires(TMember member, const TMember constMember, const TKey& key, const TValue& value, TValue& outValue)
        {
            { member.TryAdd(key, value) } -> std::same_as<bool>;
            { constMember.TryGet(key) } -> std::same_as<std::optional<TValue>>;
            { member.TryRemove(key, outValue) } -> std::same_as<bool>;

            { constMember.Contains(key) } -> std::same_as<bool>;
            { constMember.size() } -> std::same_as<size_t>;

            { constMember.Keys() } -> std::same_as<std::generator<const TKey&>>;
            { constMember.Values() } -> std::same_as<std::generator<const TValue&>>;

            { member.Clear() } -> std::same_as<void>;
        };

    namespace INTERNAL
    {
        template <typename T>
        struct _is_opt : std::false_type
        {
            using value_type = T;
        };

        template <typename T>
        struct _is_opt<std::optional<T>> : std::true_type
        {
            using value_type = T;
        };
    }

    template <typename F, typename TKey, typename TValue, typename T>
    concept CIterationInvoking =
        std::invocable<F, const TKey&, const TValue&> &&
        std::same_as<typename INTERNAL::_is_opt<std::invoke_result_t<F, const TKey&, const TValue&>>::value_type, T>;

    template <typename F, typename TKey, typename TValue>
    concept CPredicate = std::predicate<F, const TKey&, const TValue&>;

    template <CHashableKey TKey, typename TValue>
    class ICollection
    {
        STELLAR_GENERATE_INTERFACE(ICollection);

        virtual bool TryAdd(const TKey& key, const TValue& value) = 0;
        [[nodiscard]] virtual std::optional<TValue> TryGet(const TKey& key) const = 0;
        virtual bool TryRemove(const TKey& key, TValue& outValue) = 0;

        [[nodiscard]] virtual bool Contains(const TKey& key) const = 0;
        [[nodiscard]] virtual size_t size() const = 0;

        [[nodiscard]] virtual std::generator<const TKey&> Keys() const = 0;
        [[nodiscard]] virtual std::generator<const TValue&> Values() const = 0;

        virtual void Clear() = 0;
    };
    
    inline constexpr size_t DefaultNumSegments = 16;
}

STELLAR_CLANG_IGNORE_END()
