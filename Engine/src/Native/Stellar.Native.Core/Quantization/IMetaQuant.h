// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

#include "../Identifier.h"

namespace Stellar::Native::Core::Quantization
{
    class IMetaQuant
    {
        STELLAR_GENERATE_INTERFACE(IMetaQuant)

        [[nodiscard]] virtual const Identifier& GetUID() const = 0;

        STELLAR_INLINE_DEFAULTS(IMetaQuant, virtual)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::Core::Quantization, IMetaQuant)

STELLAR_CLANG_IGNORE_END()
