using Stellar.Kernel.Configuration;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.EntryPoint;
using Stellar.Kernel.Label;

namespace Stellar.Core;

[StellarEntryPoint]
public class EntryPoint(RuntimeConfiguration runtimeConfiguration)
    : StellarEntryPoint(runtimeConfiguration)
{
    public override ILabel Label { get; } = new Label.Label(new Identifier(), "Main");

    public override int Run(IContext<IModuleRunContextData> context)
    {
        throw new NotImplementedException();
    }

    public override void RequestStop(IContext<IStopContextData> context)
    {
        throw new NotImplementedException();
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }
}