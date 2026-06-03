// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "IMetaQuant.h"

namespace Stellar::Native::Core::Quantization
{
    class MetaQuant : public IMetaQuant
    {
        STELLAR_GENERATE_QUANT(MetaQuant, IMetaQuant)

    private:
        Identifier _uid;

    public:
        explicit MetaQuant(Identifier uid)
            : Base(), _uid(uid)
        {
        }

        STELLAR_INLINE_UID(override { return _uid; })

        STELLAR_INLINE_DEFAULTS(MetaQuant,, override)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::Core::Quantization, MetaQuant)
