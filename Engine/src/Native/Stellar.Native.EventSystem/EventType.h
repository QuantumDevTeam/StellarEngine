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
        Core::Label _label = Core::UnnamedUnboundLabel;

    public:
        explicit constexpr EventType(const Core::Label& label);

        ConstexprGetter(Core::Identifier, UID) override { return _label.GetUID(); }
        ConstexprGetter(Core::Label, Label) { return _label; }

        STELLAR_OVERRIDE_DEFAULTS(EventType)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::EventSystem, EventType)
