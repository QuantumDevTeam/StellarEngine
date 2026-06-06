#include "pch.h"
#include "TimerManager.h"

namespace Stellar::Native::TimeSystem
{
    TimerManager::TimerManager(Clock* clock)
        : _clock(clock)
    {
    }

    void TimerManager::AddTimer(Timer* const timer)
    {
        timer->Remaining = timer->Interval;
        timer->IsActive = true;
        _timers.TryAdd(timer->GetUID(), timer);
        std::scoped_lock lock(_mutex);
        _pendingQueue.push(timer);
    }

    bool TimerManager::StopTimer(const Core::Identifier& id)
    {
        Timer* timer;
        if (!_timers.TryRemove(id, timer)) return false;
        timer->IsActive = false;
        return true;
    }

    bool TimerManager::PauseTimer(const Core::Identifier& id) const
    {
        auto opt = _timers.TryGet(id);
        if (!opt) return false;
        Timer* timer = *opt;
        timer->IsActive = false;
        return true;
    }

    bool TimerManager::ResumeTimer(const Core::Identifier& id)
    {
        auto opt = _timers.TryGet(id);
        if (!opt) return false;
        Timer* timer = *opt;
        timer->IsActive = true;
        timer->Remaining = timer->Interval;
        std::scoped_lock lock(_mutex);
        _pendingQueue.push(timer);
        return true;
    }

    void TimerManager::PauseAll() const
    {
        for (auto& timer : _timers.Keys())
        {
            // ReSharper disable once CppExpressionWithoutSideEffects
            PauseTimer(timer);
        }
    }

    void TimerManager::ResumeAll()
    {
        for (auto& timer : _timers.Keys())
        {
            // ReSharper disable once CppExpressionWithoutSideEffects
            ResumeTimer(timer);
        }
    }

    void TimerManager::Update()
    {
        double delta = _clock->GetDeltaTime().count();
        if (delta <= 0.0) return;

        std::scoped_lock lock(_mutex);

        std::priority_queue<Timer*> newQueue;
        std::vector<Timer*> ready;

        while (!_pendingQueue.empty())
        {
            Timer* t = _pendingQueue.top();
            _pendingQueue.pop();

            if (!t->IsActive)
            {
                continue;
            }

            t->Remaining -= delta;
            bool isReady = false;
            if (t->Remaining <= 0.0)
            {
                if (t->Mode == TimerMode::OneshotConditional)
                {
                    isReady = t->Condition();
                }
                else
                {
                    isReady = true;
                }
            }

            if (isReady)
            {
                ready.push_back(t);
            }
            else
            {
                newQueue.push(t);
            }
        }
        _pendingQueue.swap(newQueue);

        for (Timer* t : ready)
        {
            if (t->Callback) t->Callback();

            if (t->Mode == TimerMode::Repeating)
            {
                t->Remaining = t->Interval;
                _pendingQueue.push(t);
            }
            else
            {
                _timers.TryRemove(t->GetUID(), t);
                delete t;
            }
        }
    }

    void TimerManager::Clear()
    {
        std::scoped_lock lock(_mutex);

        while (!_pendingQueue.empty()) _pendingQueue.pop();

        for (auto& id : _timers.Keys())
        {
            Timer* t;
            if (_timers.TryRemove(id, t) && t) delete t;
        }
    }
}
