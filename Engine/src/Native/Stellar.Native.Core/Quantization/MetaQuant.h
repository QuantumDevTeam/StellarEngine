// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "IMetaQuant.h"


namespace Stellar::Native::Core::Quantization
{
    class MetaQuant : public IMetaQuant
    {
        Identifier _uid;

    public:
        explicit MetaQuant(Identifier uid) : _uid(uid)
        {
        }

        [[nodiscard]] const Identifier& GetUID() const override { return _uid; }
    };
}
