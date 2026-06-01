// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "IMetaQuant.h"


namespace Stellar::Native::Core::Quantization
{
    class MetaQuant : public IMetaQuant
    {
        STELLAR_GENERATE_QUANT(MetaQuant)

    private:
        Identifier _uid;

    public:
        explicit MetaQuant(Identifier uid) : IMetaQuant(), _uid(uid)
        {
        }

        STELLAR_INLINE_UID(_uid)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::Core::Quantization, MetaQuant)
