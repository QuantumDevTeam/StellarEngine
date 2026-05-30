// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Registry;
using Stellar.Core.Data.Registry;

namespace Stellar.Core;

/// <inheritdoc/>
public sealed class Identifier
    : IIdentifier
{
    /// <inheritdoc/>
    public Guid UID { get; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public Identifier(Guid uid)
    {
        UID = uid;
    }

    /// <inheritdoc/>
    public void Register(IQuantumObject? registry = null)
    {
        registry ??= IdentifierRegistry.Instance;
        if (registry is IRegistry<IIdentifier> identifierRegistry)
            identifierRegistry.Register(this);
    }

    /// <inheritdoc/>
    public override string ToString() => $"Identifier#{UID.ToString()}";

    /// <inheritdoc/>
    public override int GetHashCode() => UID.GetHashCode();

    /// <inheritdoc/>
    public void Unregister(IQuantumObject? registry = null)
    {
        registry ??= IdentifierRegistry.Instance;
        if (registry is IRegistry<IIdentifier> identifierRegistry)
            identifierRegistry.Pop(this);
    }

    /// <inheritdoc/>
    public void Dispose() => Unregister();

    #region Static methods

    public static Identifier Create()
    {
        var id = new Identifier(Guid.NewGuid());
        return id;
    }

    public static Identifier CreateAndRegister()
    {
        var id = Create();
        id.Register();
        return id;
    }

    public static Identifier? Get(Guid data) => IdentifierRegistry.Instance.Get(data);

    public static Identifier? Get(byte[] data) => Get(new Guid(data));

    public static Identifier? Get(string data) => Get(new Guid(data));

    public static Identifier? Get(IIdentifier data) => Get(data.UID);

    #endregion
}