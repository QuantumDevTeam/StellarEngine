using System.Collections.Generic;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.TaskSystem;

/// <summary>
/// Represents a unit of work that can be scheduled and executed by the <see cref="ITaskScheduler"/>.
/// </summary>
/// <remarks>
/// <para>Tasks have a priority value and a list of dependencies. The scheduler ensures that a task
/// is not executed until all its dependencies have been completed.</para>
/// <para>This interface inherits <see cref="IIdentifiableQuantumObject"/> to provide a unique identifier
/// for each task, which is used for dependency tracking and lookups.</para>
/// </remarks>
public interface ITask
    : IIdentifiableQuantumObject
{
    /// <summary>
    /// Gets the priority of the task. Higher values indicate higher priority.
    /// </summary>
    /// <value>The priority level, where a larger number means more urgent execution.</value>
    /// <remarks>
    /// The scheduler uses this value to order tasks in the queue. Tasks with higher priority
    /// are dequeued and executed before those with lower priority, regardless of enqueue time.
    /// </remarks>
    int Priority { get; }

    /// <summary>
    /// Gets the list of identifiers of other tasks that must complete before this task can run.
    /// </summary>
    /// <value>A read‑only list of dependency identifiers. May be empty.</value>
    /// <remarks>
    /// <para>The scheduler checks dependencies before starting a task. If any dependency is not yet completed,
    /// the task is postponed until all dependencies are finished.</para>
    /// <para>Circular dependencies are not allowed and may cause deadlock; it is the caller's responsibility
    /// to avoid them.</para>
    /// </remarks>
    IReadOnlyList<IIdentifier> Dependencies { get; }

    /// <summary>
    /// Executes the task’s logic.
    /// </summary>
    /// <param name="context">Provides access to task data, cancellation tokens, and other execution‑related information.</param>
    /// <remarks>
    /// <para>This method is called by a worker thread when the scheduler determines that the task is ready to run
    /// (all dependencies satisfied, and it is the highest priority among available tasks).</para>
    /// <para>The <paramref name="context"/> allows the task to read/write shared data, check for cancellation,
    /// and interact with the engine’s data context.</para>
    /// </remarks>
    void Execute(ITaskContext context);
}