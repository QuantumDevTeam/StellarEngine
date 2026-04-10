using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.TaskSystem;

/// <summary>
/// Represents a wrapper around a system thread used as a simple multitasking worker in the Engine.
/// </summary>
/// <remarks>
/// <para>The <see cref="IThread"/> abstraction allows the engine to manage raw threads uniformly.
/// Each thread is typically associated with an <see cref="IThreadPool"/> and continuously executes tasks
/// from a shared <see cref="ITaskQueue"/>.</para>
/// <para>This interface inherits <see cref="IQuantumObject"/> to integrate with the Stellar quantization system.</para>
/// </remarks>
public interface IThread
    : IQuantumObject
{
    /// <summary>
    /// Gets a value indicating native thread which used in IThread
    /// </summary>
    /// <value>Managed thread Integer identifier</value>
    int ManagedThreadId { get; }

    /// <summary>
    /// Gets a value indicating whether the underlying system thread is still executing.
    /// </summary>
    /// <value><c>true</c> if the thread is alive (started and not terminated); otherwise, <c>false</c>.</value>
    bool IsAlive { get; }

    /// <summary>
    /// Starts the thread's execution.
    /// </summary>
    /// <remarks>
    /// After calling <see cref="Start"/>, the thread begins processing tasks from the associated queue.
    /// If the thread is already running, this method does nothing.
    /// </remarks>
    void Start();

    /// <summary>
    /// Blocks the calling thread until this thread terminates.
    /// </summary>
    /// <remarks>
    /// Use <see cref="Join"/> to wait for a worker thread to finish its current work and exit.
    /// If the thread is not started, this method returns immediately.
    /// </remarks>
    void Join();
}