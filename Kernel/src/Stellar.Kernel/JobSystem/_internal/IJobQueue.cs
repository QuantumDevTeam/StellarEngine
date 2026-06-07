using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.JobSystem;

/// <summary>
/// Represents a thread‑safe queue of <see cref="IJob"/> instances awaiting execution.
/// </summary>
/// <remarks>
/// <para>The queue is typically used by an <see cref="IThreadPool"/> to store tasks that are ready to be processed.
/// Implementations must ensure thread safety because multiple threads may enqueue and dequeue tasks concurrently.</para>
/// <para>This interface inherits <see cref="IQuantumObject"/> for integration with the Stellar quantization system
/// and <see cref="IDisposable"/> to release internal resources when the queue is no longer needed.</para>
/// </remarks>
public interface IJobQueue
    : IQuantumObject, IDisposable
{
    /// <summary>
    /// Gets the current number of tasks pending in the queue.
    /// </summary>
    /// <value>The total count of tasks that have been enqueued but not yet dequeued.</value>
    int TaskCount { get; }

    /// <summary>
    /// Tells if the queue is empty at the moment
    /// </summary>
    /// <returns><c>true</c> if queue is empty; otherwise, <c>false</c>.</returns>
    bool IsEmpty { get; }

    /// <summary>
    /// Adds a task to the end of the queue.
    /// </summary>
    /// <param name="task">The task to enqueue. Cannot be <c>null</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is <c>null</c>.</exception>
    /// <remarks>
    /// The method does not start execution of the task; it only places it into the queue.
    /// The actual execution is managed by an <see cref="IJobScheduler"/> or a thread pool worker.
    /// </remarks>
    void Enqueue(IJob task);

    /// <summary>
    /// Attempts to remove and return the task at the beginning of the queue.
    /// </summary>
    /// <param name="task">
    /// When this method returns, contains the dequeued task, or <c>null</c> if the queue is empty.
    /// </param>
    /// <returns><c>true</c> if a task was successfully dequeued; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method is non‑blocking. If the queue is empty, it returns <c>false</c> immediately
    /// and sets <paramref name="task"/> to <c>null</c>.
    /// </remarks>
    bool TryDequeue(out IJob task);

    /// <summary>
    /// Removes all tasks from the queue.
    /// </summary>
    /// <remarks>
    /// After calling <see cref="Clear"/>, <see cref="TaskCount"/> becomes zero.
    /// Any tasks that were in the queue are effectively abandoned and will never be executed
    /// unless they are re‑enqueued.
    /// </remarks>
    void Clear();
}