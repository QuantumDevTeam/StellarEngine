using Stellar.Core.Data.Registry;
using Stellar.Kernel;
using Stellar.Kernel.Label;
using Stellar.Core.Quantization;

namespace Stellar.Core.Label;

public class Label(IIdentifier identifier, string name)
    : RegistrableMetaQuant<Label>(identifier), ILabel
{
    public string Name { get; } = name;

    #region Get

    public static Label? Get(IIdentifier data) =>
        MetaQuantsRegistry<Label>.Instance.Get(data);

    public static Label? Get(string data) =>
        MetaQuantsRegistry<Label>.Instance.Values.FirstOrDefault(label => label.Name == data);

    #endregion

    #region implict operator

    public static implicit operator Label?(Identifier identifier) => Get(identifier);
    public static implicit operator Label?(string name) => Get(name);

    #endregion

    public static bool Exist(Guid uid) => IdentifierRegistry.Instance.Get(uid) != null;

    public override string ToString() => $"Label#{Name}";
}