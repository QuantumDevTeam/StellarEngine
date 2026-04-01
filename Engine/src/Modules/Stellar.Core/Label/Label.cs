using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Label;

namespace Stellar.Core.Label;

class Label(string name; IIdentifier? identifier = null)
    : RegistrableMetaQuant<Label>(identifier), ILabel
{
    public string Name { get; } = name;
}
