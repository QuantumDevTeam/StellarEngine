// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Job.h"

namespace Stellar::Native::JobSystem
{
    struct JobContext
    {
        STELLAR_GENERATE_BODY_PARTIAL(JobContext, explicit)

        Job* Job;
        std::stop_token StopToken;

        explicit JobContext(JobSystem::Job* job, std::stop_token stopToken)
            : Job(job), StopToken(std::move(stopToken))
        {
        }

        STELLAR_CLASS_NAME_DEF(Job)

        [[nodiscard]] STELLAR_TO_STRING() { return std::format("{}#UID({})", StaticClassName(), Job.UID.ToString()); }
        [[nodiscard]] STELLAR_HASHCODE() { return Job.UID.GetHashCode() + 1; }
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::JobSystem, JobContext)
