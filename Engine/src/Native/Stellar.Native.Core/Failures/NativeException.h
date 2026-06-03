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
        STELLAR_GENERATE_BODY_PARTIAL(NativeException, constexpr,, noexcept)

    private:
        std::string _message;
        std::exception _innerException;
        FailureLevel _failureLevel = Critical;

    public:
        explicit NativeException(std::string msg)
            : _message(std::move(msg))
        {
        }

        explicit NativeException(const std::exception& e)
            : _message(e.what()), _innerException(e)
        {
        }

        STELLAR_CLASS_NAME_DEF(NativeException)

        PropertyGetter(std::string, Message) { return _message; }
        PropertyGetter(std::exception, InnerException) { return _innerException; }
        PropertyGetter(FailureLevel, FailureLevel) { return _failureLevel; }

        // C# ToString
        STELLAR_TO_STRING()
        {
            return std::format(
                "{}"
                "#Message(\"{}\")",
                StaticClassName(),
                _message);
        }
    };

    STELLAR_GENERATE_TO_STRING(NativeException);
}

STELLAR_CLANG_IGNORE_END()
