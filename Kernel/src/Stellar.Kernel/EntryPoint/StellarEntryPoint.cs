using System;
using Stellar.Kernel.Configuration;

namespace Stellar.Kernel.EntryPoint
{
    public abstract class StellarEntryPoint : ILabeled, IDisposable
    {
        public readonly RuntimeConfiguration RuntimeConfiguration;
        
        public abstract string Name { get; }

        protected StellarEntryPoint(RuntimeConfiguration runtimeConfiguration)
        {
            RuntimeConfiguration = runtimeConfiguration;
        }

        public abstract int Run();
        public abstract void Dispose();
    }
}