using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Threading
{
    /// <summary>
    /// Provides advanced scheduling capabilities for <see cref="ITask"/> instances,
    /// including priority handling, dependency resolution, and asynchronous execution.
    /// </summary>
    /// <remarks>
    /// <para>The scheduler is responsible for ordering task execution based on priorities and dependencies.
    /// It can be paused, resumed, and reset. The scheduler works on top of a <see cref="ITaskQueue"/>
    /// (usually owned by an <see cref="IThreadPool"/>).</para>
    /// <para>This interface inherits <see cref="IQuantumObject"/> and <see cref="IDisposable"/>.</para>
    /// </remarks>
    public interface ITaskScheduler
        : IQuantumObject, IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the scheduler is currently running.
        /// </summary>
        /// <value><c>true</c> if the scheduler is actively processing tasks; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// The scheduler is considered running after <see cref="Run"/> or <see cref="RunAsync"/> is called
        /// and until it is paused (<see cref="Pause"/>) or stopped via disposal.
        /// </remarks>
        bool IsRunning { get; }

        /// <summary>
        /// Schedules a single task for execution.
        /// </summary>
        /// <param name="task">The task to schedule. Cannot be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is <c>null</c>.</exception>
        /// <remarks>
        /// The task will be placed into the internal queue and processed according to its
        /// <see cref="ITask.Priority"/> and <see cref="ITask.Dependencies"/>.
        /// </remarks>
        void Schedule(ITask task);

        /// <summary>
        /// Schedules a collection of tasks for execution.
        /// </summary>
        /// <param name="tasks">The enumeration of tasks to schedule. Cannot be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="tasks"/> is <c>null</c>.</exception>
        /// <remarks>
        /// All tasks are added to the queue. Dependencies between tasks are resolved automatically
        /// by the scheduler. Tasks with higher priority are executed first.
        /// </remarks>
        void Schedule(IEnumerable<ITask> tasks);

        /// <summary>
        /// Starts the scheduler and begins processing tasks synchronously.
        /// </summary>
        /// <remarks>
        /// This method blocks the calling thread until the scheduler is paused or stopped.
        /// Use <see cref="RunAsync"/> for non‑blocking execution.
        /// </remarks>
        void Run();

        /// <summary>
        /// НУЖЕН КОММЕНТАРИЙ
        /// </summary>
        void Stop();

        /// <summary>
        /// Starts the scheduler and begins processing tasks asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <remarks>
        /// The returned task completes when the scheduler stops.
        /// This method does not stop execution, use <see cref="Stop"/> or <see cref="IDisposable.Dispose"/> for stoping>.
        /// This method does not block the calling thread.
        /// </remarks>
        Task StartAsync();

        /// <summary>
        /// Blocks the calling thread until all scheduled tasks have completed execution.
        /// </summary>
        /// <param name="maxTime">
        /// Maximum time to wait, in seconds. Use <see cref="double.PositiveInfinity"/> to wait indefinitely.
        /// Default is <c>double.PositiveInfinity</c>.
        /// </param>
        /// <remarks>
        /// If the timeout expires before all tasks finish, the method returns anyway.
        /// This method does not cancel or interrupt running tasks.
        /// </remarks>
        void WaitForCompletion(double maxTime = Double.PositiveInfinity);

        /// <summary>
        /// Resets the scheduler to its initial state, clearing all pending tasks and resuming execution if paused.
        /// </summary>
        /// <remarks>
        /// After a reset, the scheduler stops processing, discards any queued tasks, and becomes ready
        /// to start again with <see cref="Run"/> or <see cref="StartAsync"/>.
        /// </remarks>
        void Reset();

        /// <summary>
        /// Pauses the scheduler for a specified duration or indefinitely.
        /// </summary>
        /// <param name="time">
        /// The duration to pause, in seconds. Use <see cref="double.PositiveInfinity"/> to pause indefinitely.
        /// Default is <c>double.PositiveInfinity</c>.
        /// </param>
        /// <remarks>
        /// While paused, the scheduler does not dequeue or execute any tasks.
        /// If a finite time is provided, the scheduler automatically resumes after that period.
        /// To resume early, call <see cref="Reset"/> or schedule a new task (implementation‑dependent).
        /// </remarks>
        [Obsolete("Controversial decision, can be deleted")]
        void Pause(double time = double.PositiveInfinity);

        /// <summary>
        /// Marks a task as important or normal, influencing its execution order.
        /// </summary>
        /// <param name="taskIdentifier">The unique identifier of the task to modify.</param>
        /// <param name="important">
        /// <c>true</c> to mark the task as important (higher priority effect),
        /// <c>false</c> to treat it as normal.
        /// </param>
        /// <returns>The updated <see cref="ITask"/> instance, or <c>null</c> if the task was not found.</returns>
        /// <remarks>
        /// Important tasks are always executed before normal tasks, regardless of their base priority.
        /// This method can be called even after the task has been scheduled.
        /// </remarks>
        [Obsolete("Bad practice, just use high priority for tasks")]
        ITask SetImportant(IIdentifier taskIdentifier, bool important);
    }
}