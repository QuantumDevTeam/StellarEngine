// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

#include "_Mixins/Stringable.h"
#include "FailureLevel.h"

namespace Stellar::Native::Core::Failures
{
    struct NativeException final : std::exception, Stringable<NativeException>
    {
        NativeException() noexcept = default;
        ~NativeException() noexcept override = default;

        // copy
        NativeException(const NativeException&) = default;
        NativeException(NativeException&&) noexcept = default;
        NativeException& operator=(const NativeException&) = default;
        NativeException& operator=(NativeException&&) noexcept = default;

    private:
        std::string_view _message = "Unknown exception";
        std::exception _innerException = {};
        FailureLevel _failureLevel = FailureLevel::Critical;

    public:
        explicit NativeException(const std::string_view& msg, const FailureLevel failureLevel = FailureLevel::Critical)
            : _message(std::move(msg)), _failureLevel(failureLevel)
        {
        }

        explicit NativeException(const std::exception& e)
            : _message(std::move(e.what())), _innerException(std::move(e))
        {
        }

        const std::string_view& GetMessage() const { return _message; }
        const std::exception& GetInnerException() const { return _innerException; }
        const FailureLevel& GetFailureLevel() const { return _failureLevel; }

        [[nodiscard]] std::string ToStringImpl() const noexcept override
        {
            return std::format(
                "NativeException"
                "#Message(\"{}\")",
                _message
            );
        }
    };
}

STELLAR_CLANG_IGNORE_END()
