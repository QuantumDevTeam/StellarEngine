using System;
using Stellar.Kernel.Configuration;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EntryPoint
{
    class EntryPointIdentifier
        : IIdentifier
    {
        public Guid UID { get; } = Guid.NewGuid();

        public void Register(IQuantumObject registry) =>
            throw new NotSupportedException("Registration for EntryPoint Identifier not supported");

        public void Unregister(IQuantumObject registry) =>
            throw new NotSupportedException("Unregistration for EntryPoint Identifier not supported");

        public void Dispose()
        {
        }
    }

    class EntryPointLabel
        : ILabel
    {
        public IIdentifier UID { get; }
        public string Name { get; }

        public EntryPointLabel(IIdentifier uid, string name)
        {
            UID = uid;
            Name = name;
        }

        public void Register(IQuantumObject registry) =>
            throw new NotSupportedException("Registration for EntryPoint Label not supported");

        public void Unregister(IQuantumObject registry) =>
            throw new NotSupportedException("Unregistration for EntryPoint Label not supported");

        public void Dispose()
        {
        }
    }

    /// <summary>
    /// Abstract base class for all application entry points in the Stellar Engine.
    /// </summary>
    /// <remarks>
    /// <para>An entry point is the starting and stopping unit of a module or application. It implements
    /// <see cref="ILabeled"/> (provides a name and identifier) and <see cref="IDisposable"/>.</para>
    /// <para>Derived classes must implement <see cref="Run"/> and <see cref="RequestStop"/>. The engine calls
    /// these methods with appropriate contexts that contain logging, file system access, and failure handling.</para>
    /// <para>The entry point's <see cref="UID"/> is generated randomly and should not be used for persistent identification.
    /// Use the <see cref="Label.Name"/> for human‑readable identification.</para>
    /// </remarks>
    public abstract class StellarEntryPoint
        : ILabeled, IDisposable
    {
        /// <summary>
        /// Gets the randomly generated unique identifier of this entry point.
        /// </summary>
        /// <value>An <see cref="IIdentifier"/> instance.</value>
        /// <remarks>This identifier is not stable across runs and should not be used for serialization.</remarks>
        [Obsolete("generated randomly and not use for indications")]
        public IIdentifier UID { get; } = new EntryPointIdentifier();

        /// <summary>
        /// Gets the human‑readable label of this entry point.
        /// </summary>
        /// <value>An <see cref="ILabel"/> containing the entry point's name.</value>
        public ILabel Label { get; }

        /// <summary>
        /// The runtime configuration assigned to this entry point during initialization.
        /// </summary>
        public readonly RuntimeConfiguration RuntimeConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="StellarEntryPoint"/> class.
        /// </summary>
        /// <param name="name">The human‑readable name of the entry point.</param>
        /// <param name="runtimeConfiguration">The runtime configuration obtained during engine initialization.</param>
        /// <remarks>This constructor is intended for internal engine use only.</remarks>
        [Obsolete("Used only in initialization operations encapsulated in Engine")]
        protected StellarEntryPoint(string name, RuntimeConfiguration runtimeConfiguration)
        {
            Label = new EntryPointLabel(UID, name);
            RuntimeConfiguration = runtimeConfiguration;
        }

        /// <summary>
        /// Executes the entry point's main logic.
        /// </summary>
        /// <param name="context">The execution context containing logging, working directory, and other runtime data.</param>
        /// <returns>A simple return code (0 typically indicates success).</returns>
        /// <remarks>This method is called by the engine when the module starts.</remarks>
        public abstract int Run(IContext<IModuleRunContextData> context);

        /// <summary>
        /// Requests the entry point to stop execution gracefully.
        /// </summary>
        /// <param name="context">The stopping context containing the stop reason and optional custom data.</param>
        /// <remarks>
        /// The engine calls this method when a shutdown is requested. The entry point should release resources
        /// and prepare for disposal.
        /// </remarks>
        public abstract void RequestStop(IContext<IStopContextData> context);

        /// <summary>
        /// Disposes the entry point, releasing all managed and unmanaged resources.
        /// </summary>
        /// <remarks>Called during module uninitialization.</remarks>
        public abstract void Dispose();
    }
}