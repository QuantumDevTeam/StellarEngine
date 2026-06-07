// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "../Stellar.Native.Core/Quantization/IMetaQuant.h"

namespace Stellar::Native::TimeSystem
{
    inline constexpr double Microseconds = 1000000.0;
    inline constexpr double Milliseconds = 1000.0;

    using Duration = std::chrono::duration<double, std::milli>;
    using TimePoint = std::chrono::steady_clock::time_point;

    class Clock final : Core::Quantization::IMetaQuant
    {
        Core::Identifier _uid;
        bool _isRunning = false;
        bool _isPaused = false;

        TimePoint _startTime{};
        TimePoint _lastTick{};

        Duration _totalTime = Duration(0.0);
        Duration _unscaledDeltaTime = Duration(0.0);
        Duration _deltaTime = Duration(0.0);
        uint64_t _frameCount = 0;

        mutable std::mutex _mutex;
        mutable std::condition_variable _cv;

        [[nodiscard]] Duration GetTickDelayMs() const { return Duration(Milliseconds / TPS); }

    public:
        bool BlockOnPause = false;
        Duration MaxDeltaTime = Duration(Milliseconds * 0.1);
        uint32_t TPS = 60;
        float Speed = 1.0f;

        Clock(uint32_t tps = -1);
        ~Clock() override;

        STELLAR_DEFAULT_COPY_OPERATORS(Clock);

        PropertyGetter(Core::Identifier, UID) { return _uid; }
        PropertyGetter(bool, IsRunning) { return _isRunning; }
        PropertyGetter(bool, IsPaused) { return _isPaused; }
        PropertyGetter(Duration, TotalTime) { return _totalTime; }
        PropertyGetter(Duration, UnscaledDeltaTime) { return _deltaTime; }
        PropertyGetter(Duration, DeltaTime) { return _deltaTime; }
        PropertyGetter(uint64_t, FrameCount) { return _frameCount; }
        PropertyGetter(TimePoint, StartTime) { return _startTime; }
        PropertyGetter(TimePoint, LastTick) { return _lastTick; }

        void Reset();
        void Start();
        void Stop();
        void Pause();
        void Resume();

        void WaitUntilResumed() const;
        void Tick();

        STELLAR_OVERRIDE_DEFAULTS(Clock)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::TimeSystem, Clock)
