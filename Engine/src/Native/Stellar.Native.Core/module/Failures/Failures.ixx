// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
module;

#include "pch.h"

#include "FailureLevel.h"
#include "NativeException.h"

export module Stellar.Native.Core.Failures;

export namespace Stellar::Native::Core::Failures
{
    using Failures::to_string;

    // FailureLevel
    using Failures::FailureLevel;
    using Failures::NonCritical;
    using Failures::Warning;
    using Failures::Error;
    using Failures::Critical;

    // NativeException
    using Failures::NativeException;
}

export namespace std
{
    // not implemented
    // template <>
    // using std::hash<Stellar::Native::Core::Failures::NativeException>;
}
