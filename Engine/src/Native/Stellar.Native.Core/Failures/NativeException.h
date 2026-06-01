// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

#include "FailureLevel.h"


namespace Stellar::Native::Core::Failures
{
    struct NativeException : std::exception
    {
        STELLAR_GENERATE_BODY(NativeException, constexpr)
        
        std::string Message;
        std::exception InnerException;
        FailureLevel FailureLevel = Critical;

        constexpr NativeException(std::string msg) : Message(std::move(msg))
        {
        }

        NativeException(const std::exception& e) : Message(e.what()), InnerException(e)
        {
        }
        
        // C# ToString
        STELLAR_TO_STRING()
        {
            return std::string("NativeException#Message(\"" + Message + "\")");
        }
    };
    
    STELLAR_GENERATE_TO_STRING(NativeException);
}

STELLAR_CLANG_IGNORE_END()
