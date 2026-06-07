// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <functional>
#include <shared_mutex>

#include "../../../Identifier.h"

namespace Stellar::Native::Core::Data::Collections
{
    template <typename T>
    class ConcurrentVector
    {
        Identifier _uid = Identifier::Create();

        mutable std::shared_mutex _mutex;
        std::vector<T> _data{};

    public:
        STELLAR_GENERATE_BODY_PARTIAL(ConcurrentVector)

        void PushBack(const T& value);
        bool TryGet(size_t index, T& outValue) const;
        bool TryRemove(size_t index, T& outValue);

        void Sort(std::function<bool(const T&, const T&)> comparator);
        size_t size() const;

        void Clear();

        STELLAR_DEFAULTS(ConcurrentVector);
    };
}
