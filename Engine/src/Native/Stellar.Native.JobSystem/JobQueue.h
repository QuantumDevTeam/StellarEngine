// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "../Stellar.Native.Core/Data/Collections/ConcurrentQueue/ConcurrentPriorityQueue.h"

namespace Stellar::Native::JobSystem
{
    template <typename T, typename Compare = std::less<T>>
    struct JobQueue
    {
        STELLAR_GENERATE_BODY_PARTIAL(JobQueue, explicit)

    private:
        Core::Identifier _uid;
        Core::Data::Collections::ConcurrentPriorityQueue<T, Compare> _queue;

    public:
    };
}
