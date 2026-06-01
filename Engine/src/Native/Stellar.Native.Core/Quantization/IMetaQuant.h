// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wpadded"

#include "../Identifier.h"

#include <memory>


namespace Stellar::Native::Core::Quantization
{
    class IMetaQuant
    {
    public:
        STELLAR_PREPARE_INTERFACE_FULL(IMetaQuant);

        [[nodiscard]] virtual const Identifier& GetUID() const = 0;

        // C# ToString
        [[nodiscard]] virtual std::string ToString() const noexcept
        {
            return std::string(typeid(*this).name()) + "#" + to_string(GetUID());
        }

        // C# GetHashCode
        [[nodiscard]] virtual uint64_t GetHashCode() const noexcept
        {
            return GetUID().GetHashCode();
        }

        [[nodiscard]] virtual bool operator==(const IMetaQuant& other) const noexcept
        {
            return GetHashCode() == other.GetHashCode();
        }
    };

    STELLAR_DEFINE_TO_STRING(IMetaQuant);
}

STELLAR_DEFINE_HASHER(Stellar::Native::Core::Quantization::IMetaQuant);

#pragma clang diagnostic pop
