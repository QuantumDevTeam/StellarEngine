// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <queue>

#include "Clock.h"
#include "Timer.h"
#include "../Stellar.Native.Core/Data/Collections/ConcurrentMap/ConcurrentUnorderedMap.h"

namespace Stellar::Native::TimeSystem
{
    class TimerManager final : Core::Quantization::IMetaQuant
    {
        Core::Identifier _uid;
        Clock* _clock;
        Core::Data::Collections::ConcurrentUnorderedIdentifierMap<Timer*> _timers; // NOLINT(clang-diagnostic-unused-private-field) WHAT?! X4
        std::priority_queue<Timer*> _pendingQueue;
        mutable std::mutex _mutex;

    public:
        TimerManager(Clock* clock);
        STELLAR_DECONSTRUCT(TimerManager, override);

        STELLAR_DEFAULT_COPY_OPERATORS(TimerManager);

        PropertyGetter(Core::Identifier, UID) { return _uid; }

        void AddTimer(Timer* timer);
        bool StopTimer(const Core::Identifier& id);
        bool PauseTimer(const Core::Identifier& id) const;
        bool ResumeTimer(const Core::Identifier& id);

        void PauseAll() const;
        void ResumeAll();

        void Update();
        void Clear();
    };
}
