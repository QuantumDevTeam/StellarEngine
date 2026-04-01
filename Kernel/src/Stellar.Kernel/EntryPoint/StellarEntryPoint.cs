using System;
using Stellar.Kernel.Configuration;
using Stellar.Kernel.Label;

namespace Stellar.Kernel.EntryPoint
{
    public abstract class StellarEntryPoint : ILabeled, IDisposable
    {
        public readonly RuntimeConfiguration RuntimeConfiguration;
        
        public abstract ILabel Label { get; }

        protected StellarEntryPoint(RuntimeConfiguration runtimeConfiguration)
        {
            RuntimeConfiguration = runtimeConfiguration;
        }

        public abstract int Run();
        public abstract void Dispose();
    }
}