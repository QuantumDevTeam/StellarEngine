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
        NativeException() noexcept = default;
        ~NativeException() noexcept override = default;
        STELLAR_DEFAULT_COPY_OPERATORS(NativeException);

    private:
        std::string_view _message = "Unknown exception";
        std::exception _innerException = {};
        FailureLevel _failureLevel = Critical;

    public:
        explicit NativeException(const std::string_view& msg, const FailureLevel failureLevel = Critical)
            : _message(std::move(msg)), _failureLevel(failureLevel)
        {
        }

        explicit NativeException(const std::exception& e)
            : _message(std::move(e.what())), _innerException(std::move(e))
        {
        }

        STELLAR_CLASS_NAME_DEF(NativeException)

        PropertyGetter(const std::string_view&, Message) { return _message; }
        PropertyGetter(const std::exception&, InnerException) { return _innerException; }
        PropertyGetter(const FailureLevel&, FailureLevel) { return _failureLevel; }

        // C# ToString
        STELLAR_TO_STRING()
        {
            return std::format(
                "{}"
                "#Message(\"{}\")",
                StaticClassName(),
                _message
            );
        }

        STELLAR_SPACESHIP(NativeException);
    };

    STELLAR_GENERATE_TO_STRING(NativeException);
}

STELLAR_CLANG_IGNORE_END()
