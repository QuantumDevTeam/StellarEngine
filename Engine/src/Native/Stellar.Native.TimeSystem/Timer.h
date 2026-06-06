// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <functional>

#include "Quantization/IMetaQuant.h"

namespace Stellar::Native::TimeSystem
{
    enum class TimerMode : uint8_t
    {
        Oneshot,
        OneshotConditional,
        Repeating,
    };

    inline const char* to_string(TimerMode e)
    {
        switch (e)
        {
        case TimerMode::Oneshot: return "Oneshot";
        case TimerMode::Repeating: return "Repeating";
        case TimerMode::OneshotConditional: return "Conditional";
        default: std::unreachable();
        }
    }

    struct Timer final : Core::Quantization::IMetaQuant
    {
        STELLAR_GENERATE_BODY(Timer, IMetaQuant)

    private:
        Core::Identifier _uid;

    public:
        double Interval;
        double Remaining;

        std::function<void()> Callback;
        std::function<bool()> Condition;
        TimerMode Mode = TimerMode::Oneshot;

        bool UseRealTime = false;
        bool IsActive = true;

        PropertyGetter(Core::Identifier, UID) { return _uid; }

        STELLAR_CLASS_NAME_DEF(Timer);

        [[nodiscard]] STELLAR_TO_STRING() override
        {
            return std::format("{}#UID{}#Mode{}", StaticClassName(), _uid.ToString(), to_string(Mode));
        }

        [[nodiscard]] STELLAR_HASHCODE() override { return _uid.GetHashCode(); }

        bool operator<(const Timer& other) const
        {
            return Remaining > other.Remaining;
        }
    };
}
