// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Job.h"

namespace Stellar::Native::JobSystem
{
    struct JobContext
    {
        Job Job;
        std::stop_token StopToken;
    };
}
