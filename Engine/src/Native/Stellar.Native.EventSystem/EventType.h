// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Label.h"
#include "Quantization/IMetaQuant.h"

namespace Stellar::Native::EventSystem
{
    struct EventType : Core::Quantization::IMetaQuant
    {
        STELLAR_GENERATE_BODY(EventType, IMetaQuant)

    private:
        Core::Label Label = Core::UnnamedUnboundLabel;

    public:
        constexpr EventType(const Core::Label& label);

        STELLAR_INLINE_UID(override { return Label.UID; })
        STELLAR_INLINE_LABEL({ return Label; })

        STELLAR_DEFAULTS(EventType,, override)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::EventSystem, EventType)
