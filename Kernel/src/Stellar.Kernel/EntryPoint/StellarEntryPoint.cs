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
    }

    public abstract class StellarEntryPoint
        : ILabeled, IDisposable
    {
        /// <summary>
        /// UID for EntryPoint
        /// </summary>
        [Obsolete("generated randomly and not use for indications")]
        public IIdentifier UID { get; } = new EntryPointIdentifier();

        /// <summary>
        /// EntryPoint Label
        /// </summary>
        public ILabel Label { get; }

        /// <summary>
        /// Runtime configuration for EntryPoint
        /// </summary>
        public readonly RuntimeConfiguration RuntimeConfiguration;

        [Obsolete("Used only in initialization operations encapsulated in Engine")]
        protected StellarEntryPoint(string name, RuntimeConfiguration runtimeConfiguration)
        {
            Label = new EntryPointLabel(UID, name);
            RuntimeConfiguration = runtimeConfiguration;
        }

        /// <summary>
        /// Just run method
        /// </summary>
        /// <param name="context">Running context</param>
        /// <returns>Simple return code</returns>
        public abstract int Run(IContext<IModuleRunContextData> context);

        /// <summary>
        /// Requesting of stoping execution
        /// </summary>
        /// <param name="context">Stopping context</param>
        public abstract void RequestStop(IContext<IStopContextData> context);

        public abstract void Dispose();
    }
}