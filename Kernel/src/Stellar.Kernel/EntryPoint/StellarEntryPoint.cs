using System;
using Stellar.Kernel.Configuration;

namespace Stellar.Kernel.EntryPoint
{
    public abstract class StellarEntryPoint : IDisposable
    {
        public readonly RuntimeConfiguration RuntimeConfiguration;

        protected StellarEntryPoint(RuntimeConfiguration runtimeConfiguration)
        {
            RuntimeConfiguration = runtimeConfiguration;
        }

        public abstract int Run();
        public abstract void Dispose();
    }
}