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

        static const char* StaticClassName() { return "Native.""IMetaQuant"; }
        virtual const char* GetClassNameW() const { return StaticClassName(); }

        [[nodiscard]] virtual std::string ToString() const noexcept
        {
            return std::format("{}#UID({})", IMetaQuant::StaticClassName(), to_string(GetUID()));
        }

        [[nodiscard]] virtual uint64_t GetHashCode() const noexcept { return GetUID().GetHashCode(); }
        auto operator<=>(const IMetaQuant&) const noexcept = default;
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::Core::Quantization, IMetaQuant)

STELLAR_CLANG_IGNORE_END()
