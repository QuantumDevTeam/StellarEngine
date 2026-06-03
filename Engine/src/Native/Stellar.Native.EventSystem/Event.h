// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "EventType.h"

namespace Stellar::Native::EventSystem
{
    struct Event : Core::Quantization::IMetaQuant
    {
        STELLAR_GENERATE_BODY(Event, IMetaQuant)

    private:
        Core::Identifier _uid;
        EventType _type;
        uint64_t _timestamp;

    public:
        constexpr Event(const EventType& type, uint64_t timestamp);
        
        STELLAR_INLINE_UID(override { return _uid; })

        STELLAR_DEFAULTS(Event,, override)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::EventSystem, Event)
