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
        void* Data = nullptr;
        
        constexpr Event(const EventType& type, uint64_t timestamp);

        ConstexprGetter(Core::Identifier, UID) override { return _uid; }
        ConstexprGetter(EventType, EventType) { return _type; }
        ConstexprGetter(uint64_t, Timestamp) { return _timestamp; }

        STELLAR_OVERRIDE_DEFAULTS(Event)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::EventSystem, Event)
