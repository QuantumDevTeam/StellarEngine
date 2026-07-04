// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

namespace Stellar::Native::Core::Failures
{
    enum class FailureLevel : uint8_t
    {
        NonCritical = 0b00001,
        Warning = 0b00011,
        Error = 0b00111,
        Critical = 0b01111
    };
}
