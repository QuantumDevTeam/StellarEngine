using Stellar.Kernel;
using Stellar.Kernel.Label;
using Stellar.Core.Quantization;

namespace Stellar.Core.Label;

public class Label(string name, IIdentifier? identifier = null)
    : RegistrableMetaQuant<Label>(identifier), ILabel
{
    public string Name { get; } = name;
}