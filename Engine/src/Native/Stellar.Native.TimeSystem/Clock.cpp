#include "pch.h"
#include "Clock.h"

namespace Stellar::Native::TimeSystem
{
    Clock::Clock(uint32_t tps)
        : _uid(Core::Identifier::Create()), TPS(tps)
    {
    }

    Clock::~Clock()
    {
        Stop();
    }

    void Clock::Reset()
    {
        _startTime = std::chrono::steady_clock::now();
        _lastTick = _startTime;
        _totalTime = Duration(0.0);
        _deltaTime = Duration(0.0);
        _frameCount = 0;
    }

    void Clock::Start()
    {
        if (_isRunning) return;
        _isRunning = true;
        _isPaused = false;
        Reset();
    }

    void Clock::Stop()
    {
        if (!_isRunning) return;
        _isRunning = false;
        _isPaused = false;
    }

    void Clock::Pause()
    {
        if (!_isRunning) return;
        {
            std::scoped_lock lock(_mutex);
            _isPaused = true;
        }
    }

    void Clock::Resume()
    {
        if (!_isRunning) return;
        {
            std::scoped_lock lock(_mutex);
            if (!_isPaused) return;
            _isPaused = false;
            _lastTick = std::chrono::steady_clock::now();
        }
        _cv.notify_one();
    }

    void Clock::WaitUntilResumed() const
    {
        if (!BlockOnPause) return;
        std::unique_lock lock(_mutex);
        _cv.wait(lock, [this] { return !_isPaused || !_isRunning; });
    }

    void Clock::Tick()
    {
        if (!_isRunning) return;

        TimePoint now = std::chrono::steady_clock::now();

        _unscaledDeltaTime = now - _lastTick;
        if (_unscaledDeltaTime > MaxDeltaTime) _unscaledDeltaTime = MaxDeltaTime;
        _deltaTime = _unscaledDeltaTime * Speed;

        _totalTime += _deltaTime;

        if (TPS > 0 && !_isPaused)
        {
            std::this_thread::sleep_until(
                _lastTick + GetTickDelayMs());
        }

        _frameCount++;
        _lastTick = now;
    }

    uint64_t Clock::GetHashCode() const noexcept
    {
        return _uid.GetHashCode();
    }

    std::string Clock::ToString() const noexcept
    {
        return std::format(
            "{}"
            "#UID{}",
            StaticClassName(),
            _uid.ToString()
        );
    }
}
