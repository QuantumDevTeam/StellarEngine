// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
#include "../Stellar.Native.Core/Identifier.h"

STELLAR_CLANG_IGNORE("-Wpadded")

#include <functional>

namespace Stellar::Native::JobSystem
{
    struct JobContext;
}

namespace Stellar::Native::Core
{
    struct Identifier;
}

namespace Stellar::Native::JobSystem
{
    struct Job
    {
        STELLAR_GENERATE_BODY_PARTIAL(Job, explicit)

        Core::Identifier UID;
        float Priority;
        std::function<void(const JobContext&)> Callback;
        std::span<const Core::Identifier> Dependencies;

        Job(float priority,
            std::function<void(const JobContext&)> callback,
            std::span<const Core::Identifier> dependencies
        ) : UID(Core::Identifier::Create()),
            Priority(priority),
            Callback(std::move(callback)),
            Dependencies(dependencies)
        {
        }

        STELLAR_CLASS_NAME_DEF(Job)

        [[nodiscard]] STELLAR_TO_STRING() { return std::format("{}#UID({})", StaticClassName(), UID.ToString()); }
        [[nodiscard]] STELLAR_HASHCODE() { return UID.GetHashCode(); }

        bool operator<(const Job& other) const
        {
            return Priority > other.Priority;
        }
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::JobSystem, Job)

STELLAR_CLANG_IGNORE_END()
