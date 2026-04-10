using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.TaskSystem;

/// <summary>
/// Manages a fixed‑size pool of worker threads that execute tasks from a shared queue.
/// </summary>
/// <remarks>
/// <para>The thread pool is created during engine initialization and lives for the entire application lifetime.
/// It owns an <see cref="ITaskQueue"/> that holds pending tasks. Worker threads constantly dequeue and execute
/// tasks until the pool is stopped.</para>
/// <para>This interface inherits <see cref="IQuantumObject"/> and <see cref="IDisposable"/> to allow proper cleanup
/// of thread resources.</para>
/// </remarks>
public interface IThreadPool
    : IQuantumObject, IDisposable
{
    /// <summary>
    /// Gets the task queue that stores pending work items.
    /// </summary>
    /// <value>The <see cref="ITaskQueue"/> instance used by the pool.</value>
    /// <remarks>
    /// External components can enqueue tasks directly into this queue.
    /// The pool’s threads will automatically dequeue and execute them.
    /// </remarks>
    ITaskQueue TaskQueue { get; }

    /// <summary>
    /// Gets the current number of threads in the pool.
    /// </summary>
    /// <value>The total number of worker threads, regardless of their state (idle or busy).</value>
    int ThreadCount { get; }

    /// <summary>
    /// Starts the thread pool with the specified number of worker threads.
    /// </summary>
    /// <param name="threadCount">Number of threads to create and start. Must be greater than zero.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="threadCount"/> is less than or equal to zero.</exception>
    /// <remarks>
    /// If the pool is already running, this method stops workers it first (waiting for pending tasks by default)
    /// and then restarts with the new thread count. Each thread begins executing <see cref="ITask"/> instances
    /// from <see cref="TaskQueue"/>.
    /// </remarks>
    void Start(int threadCount);

    /// <summary>
    /// Stops the thread pool, optionally waiting for currently executing tasks to finish.
    /// </summary>
    /// <param name="waitForTasks">
    /// If <c>true</c>, the method blocks until all running tasks complete;
    /// if <c>false</c>, running tasks are aborted (implementation‑defined).
    /// </param>
    /// <remarks>
    /// After stopping, no new tasks are taken from the queue, and all worker threads exit.
    /// The pool can be restarted later with <see cref="Start"/>.
    /// </remarks>
    void Stop(bool waitForTasks = true);

    /// <summary>
    /// Changes the number of worker threads in the pool.
    /// </summary>
    /// <param name="newThreadCount">The new desired thread count. Must be greater than zero.</param>
    /// <param name="waitForTasks">
    /// If <c>true</c>, the method blocks until currently executing tasks finish before resizing;
    /// if <c>false</c>, the resize occurs immediately, possibly aborting running tasks.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="newThreadCount"/> is less than or equal to zero.</exception>
    /// <remarks>
    /// <para>If <paramref name="newThreadCount"/> equals the current <see cref="ThreadCount"/>, this method does nothing.
    /// If it is larger, new threads are added; if smaller, excess threads are stopped.</para>
    /// <para>The queue and pending tasks are preserved during the resize operation.</para>
    /// </remarks>
    void Resize(int newThreadCount, bool waitForTasks = true);
}