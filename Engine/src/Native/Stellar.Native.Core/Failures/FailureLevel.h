// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

namespace Stellar::Native::Core::Failures
{
    struct FailureLevel
    {
        bool IsEnabled = true;
        bool IsLoggable = true;
        bool IsStopExecute = true;
        bool IsCritical = true;
        bool ShouldTerminate = true;
    };

    inline constexpr FailureLevel NonCritical{true, false, false, false, false};
    inline constexpr FailureLevel Warning{true, true, false, false, false};
    inline constexpr FailureLevel Error{true, true, true, true, false};
    inline constexpr FailureLevel Critical{};
}
