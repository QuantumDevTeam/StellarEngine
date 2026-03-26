using Stellar.Kernel.Configuration;
using Stellar.Kernel.EntryPoint;

namespace Stellar.Core;

[StellarEntryPoint]
public class EntryPoint(RuntimeConfiguration runtimeConfiguration)
    : StellarEntryPoint(runtimeConfiguration)
{
    public override string Name => "Main";

    public override int Run()
    {
        return 0;
    }

    public override void Dispose()
    {
    }
}