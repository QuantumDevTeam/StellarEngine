using Stellar.Kernel.Configuration;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.EntryPoint;

namespace Stellar.Core;

[StellarEntryPoint]
public sealed class EntryPoint
    : StellarEntryPoint
{
    [Obsolete("Used only in initialization operations encapsulated in Engine")]
    public EntryPoint(RuntimeConfiguration runtimeConfiguration)
        : base("S.C/MainEntry", runtimeConfiguration)
    {
    }

    public override int Run(IContext context)
    {
        throw new NotImplementedException();
    }

    public override void RequestStop(IContext context)
    {
        throw new NotImplementedException();
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }
}