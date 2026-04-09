using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Provides a fluent API for catching, handling, and recovering from failures.
    /// </summary>
    /// <remarks>
    /// <para>This interface mimics the familiar try/catch/finally pattern but integrated with the Stellar failure system.
    /// It is an identifiable and registrable quantum object, allowing it to be reused or stored.</para>
    /// <para>The methods return the same <see cref="IFailureCatch"/> instance to enable method chaining.</para>
    /// </remarks>
    /// <example>
    /// Typical usage:
    /// <code>
    /// failureCatch
    ///     .Try(c => SomeRiskyOperation())
    ///     .Except&lt;IOException&gt;((c, ex) => Console.WriteLine("IO error"))
    ///     .Except&lt;TimeoutException&gt;((c, ex) => Console.WriteLine("Timeout"))
    ///     .Finally(c => Cleanup())
    ///     .Handle();
    /// </code>
    /// </example>
    public interface IFailureCatch
        : IIdentifiableQuantumObject, IRegistrableQuantumObject
    {
        /// <summary>
        /// Executes an action within the failure catch context.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        void TryAction(Action action);

        /// <summary>
        /// Executes an action that receives this failure catch context.
        /// </summary>
        /// <param name="action">The action to execute, providing access to the catch context.</param>
        void TryAction(Action<IFailureCatch> action);

        /// <summary>
        /// Begins a try block that can be followed by <see cref="Except{T}"/> or <see cref="Finally"/>.
        /// </summary>
        /// <param name="action">The action to attempt.</param>
        /// <returns>The same catch context for chaining.</returns>
        IFailureCatch Try(Action<IFailureCatch> action);

        /// <summary>
        /// Begins a try block that returns a value (the return is ignored, used for side effects).
        /// </summary>
        /// <param name="func">The function to attempt.</param>
        /// <returns>The same catch context for chaining.</returns>
        IFailureCatch Try(Func<IFailureCatch> func);

        /// <summary>
        /// Adds an exception handler for a specific exception type.
        /// </summary>
        /// <typeparam name="T">The type of exception to catch.</typeparam>
        /// <param name="action">The action to execute when the exception occurs, receiving the context and the exception.</param>
        /// <returns>The same catch context for chaining.</returns>
        IFailureCatch Except<T>(Action<IFailureCatch, T> action) where T : Exception;

        /// <summary>
        /// Adds an exception handler for a specific exception type that returns a value.
        /// </summary>
        /// <typeparam name="T">The type of exception to catch.</typeparam>
        /// <param name="func">The function to execute, receiving the context and the exception; the return value is ignored.</param>
        /// <returns>The same catch context for chaining.</returns>
        IFailureCatch Except<T>(Func<IFailureCatch, T> func) where T : Exception;

        /// <summary>
        /// Adds a finally block that executes regardless of whether an exception occurred.
        /// </summary>
        /// <param name="action">The action to execute in the finally block.</param>
        /// <returns>The same catch context for chaining.</returns>
        IFailureCatch Finally(Action<IFailureCatch> action);

        /// <summary>
        /// Adds a finally block that returns a value (ignored).
        /// </summary>
        /// <param name="func">The function to execute in the finally block.</param>
        /// <returns>The same catch context for chaining.</returns>
        IFailureCatch Finally(Func<IFailureCatch> func);

        /// <summary>
        /// Processes the registered try/catch/finally blocks and returns the catch context.
        /// </summary>
        /// <returns>The same catch context after handling.</returns>
        /// <remarks>
        /// This method triggers the actual execution flow. It should be called after all
        /// <see cref="Try"/>, <see cref="Except{T}"/>, and <see cref="Finally"/> definitions.
        /// </remarks>
        IFailureCatch Handle();
    }
}