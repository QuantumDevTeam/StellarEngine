using System;
using Stellar.Kernel.Configuration;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Label;

namespace Stellar.Kernel.EntryPoint
{
    public abstract class StellarEntryPoint
        : ILabeled, IDisposable
    {
        public abstract ILabel Label { get; }

        public readonly RuntimeConfiguration RuntimeConfiguration;

        protected StellarEntryPoint(RuntimeConfiguration runtimeConfiguration)
        {
            RuntimeConfiguration = runtimeConfiguration;
        }

        public abstract int Run(IContext<IModuleRunContextData> context);
        public abstract void RequestStop(IContext<IStopContextData> context);
        public abstract void Dispose();
    }
}