// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <shared_mutex>
#include <queue>

#include "../ICollection.h"
#include "../../../Identifier.h"

namespace Stellar::Native::Core::Data::Collections
{
    template <typename T, typename Compare = std::less<T>>
    struct ConcurrentPriorityQueue
    {
    private:
        Identifier _uid = Identifier::Create();

    protected:
        // data type
        struct Segment
        {
            std::priority_queue<T, std::vector<T>, Compare> heap;
            mutable std::shared_mutex mutex;
            std::condition_variable_any cv;
        };

        // stored data
        std::vector<Segment> _segments;

        // Gets Segment by stored data
        [[nodiscard]] std::size_t GetSegmentIndex(const T& value) const noexcept;

    public:
        explicit ConcurrentPriorityQueue(size_t numSegments = DefaultNumSegments);
        STELLAR_DECONSTRUCT(ConcurrentPriorityQueue);
        STELLAR_DEFAULT_COPY_OPERATORS(ConcurrentPriorityQueue);

        void Push(T value);
        std::optional<T> TryPop();
        T PopWait(const std::stop_token& st);

        size_t size() const;
        void Clear();

        STELLAR_DEFAULTS(ConcurrentPriorityQueue);
    };
}
