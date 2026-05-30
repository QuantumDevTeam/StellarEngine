using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    /// <summary>
    /// Represents an execution context for an operation inside the engine.
    /// </summary>
    /// <remarks>
    /// <para>The context is passed to all executable engine methods: starting/stopping entry points (<see cref="EntryPoint.StellarEntryPoint"/>),
    /// scene updates, rendering, event handling, etc.</para>
    /// <para>It contains the sender (initiator of the call) and user data specific to the operation.</para>
    /// <para>Depending on the target platform (<c>NETSTANDARD2_0</c> or newer), properties may be nullable.</para>
    /// </remarks>
    /// <example>
    /// Using context in an entry point's <c>Run</c> method:
    /// <code>
    /// public override int Run(IContext context)
    /// {
    ///     var logger = context.GetData&lt;IModuleRunContextData&gt;()?.Logger;
    ///     logger?.Info("Engine started");
    ///     return 0;
    /// }
    /// </code>
    /// </example>
    public interface IContext
        : IQuantumObject
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Quantum object that initiated this context's execution.
        /// </summary>
        /// <value>The sender (e.g., entry point, system thread, or user code).</value>
        /// <remarks>May be <c>null</c> if the context was created by the system without an explicit sender.</remarks>
        IQuantumObject Sender { get; }
        
        /// <summary>
        /// Operation‑specific context data.
        /// </summary>
        /// <value>An <see cref="IContextData"/> instance or <c>null</c>.</value>
        IContextData Data { get; }
#else
#nullable enable
        /// <summary>
        /// Quantum object that initiated this context's execution.
        /// </summary>
        /// <value>The sender, or <c>null</c> if no explicit sender exists.</value>
        IQuantumObject? Sender { get; }

        /// <summary>
        /// Operation‑specific context data.
        /// </summary>
        /// <value>An <see cref="IContextData"/> instance or <c>null</c>.</value>
        IContextData? Data { get; }
#endif
    }
}