using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Label;
using Stellar.Kernel.Data.Registry;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Label;

/// <inheritdoc cref="ILabel" />
public sealed class Label
    : MetaQuant, ILabel
{
    /// <inheritdoc/>
    public string Name { get; }

    private Label(IIdentifier identifier, string name)
        : base(identifier)
    {
        Name = name;
    }

    /// <inheritdoc/>
    public void Register(IQuantumObject? registry = null)
    {
        registry ??= LabelRegistry.Instance;
        if (registry is IRegistry<ILabel> identifierRegistry)
            identifierRegistry.Register(this);
    }

    /// <inheritdoc/>
    public override string ToString() => $"Label#{Name}";

    /// <inheritdoc/>
    public void Unregister(IQuantumObject? registry = null)
    {
        registry ??= LabelRegistry.Instance;
        if (registry is IRegistry<ILabel> identifierRegistry)
            identifierRegistry.Pop(UID);
    }

    /// <inheritdoc/>
    public void Dispose() => Unregister();

    #region Static Methoods

    public static Label? Create(string name, IIdentifier? identifier = null, bool register = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        identifier ??= new Identifier(Guid.NewGuid());
        var label = new Label(identifier, name);
        if (register) LabelRegistry.Instance.Register(label);
        return label;
    }

    public static Label? Get(IIdentifier identifier) => LabelRegistry.Instance.Get(identifier);
    public static Label? Get(string name) => LabelRegistry.Instance.GetByName(name);

    #endregion

    public static implicit operator Label?(string name) => Get(name) ?? Create(name);
}