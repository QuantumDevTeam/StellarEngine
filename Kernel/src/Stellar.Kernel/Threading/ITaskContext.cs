using System.Threading;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Threading
{
    /// <summary>
    /// Provides execution context for an <see cref="ITask"/>, including task‑specific data,
    /// cancellation support, and access to the task instance itself.
    /// </summary>
    /// <remarks>
    /// <para>This interface extends <see cref="IContext"/> to reuse the general data context mechanism of the Stellar kernel.
    /// Each scheduled task receives its own context instance when <see cref="ITask.Execute"/> is called.</para>
    /// <para>The cancellation token can be triggered by the scheduler (e.g., when the engine shuts down)
    /// to allow long‑running tasks to abort gracefully.</para>
    /// </remarks>
    public interface ITaskContext
        : IContext
    {
        /// <summary>
        /// Gets the task instance that is being executed.
        /// </summary>
        /// <value>The original <see cref="ITask"/> object.</value>
        ITask Task { get; }

        /// <summary>
        /// Gets a cancellation token that signals when the task execution should be aborted.
        /// </summary>
        /// <value>A <see cref="CancellationToken"/> that can be monitored for cancellation requests.</value>
        /// <remarks>
        /// The task should periodically check <see cref="CancellationToken.IsCancellationRequested"/>
        /// and exit gracefully if cancellation is requested, freeing any held resources.
        /// </remarks>
        CancellationToken CancellationToken { get; }
    }
}