using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Quantization;
using Stellar.Kernel.Data.Collections;

namespace Stellar.Core.Data.Collections;

/// <summary>
/// Represents a quantum object that can contain and manage other identifiable quantum objects.
/// Roster can store other Rosters in the Graph structure
/// </summary>
/// <inheritdoc/>
public class Roster<T>
    : WritableTable<T>
    where T : IIdentifiableQuantumObject
{
    public ConcurrentIdentifierMap<Roster<T>> Branches { get; }

    #region Constructors

    public Roster(MetaQuant meta, FastConcurrentIdentifierMap<T> data,
        Dictionary<IIdentifier, Roster<T>> branches)
        : base(meta, data)
    {
        Branches = new ConcurrentIdentifierMap<Roster<T>>(branches);
    }

    public Roster(MetaQuant metaData, Dictionary<IIdentifier, T> data,
        Dictionary<IIdentifier, Roster<T>> branches)
        : this(metaData, new FastConcurrentIdentifierMap<T>(data), branches)
    {
    }

    public Roster(MetaQuant metaData, Dictionary<IIdentifier, T> data)
        : this(metaData, data, [])
    {
    }

    public Roster(MetaQuant metaData)
        : this(metaData, [])
    {
    }

    public Roster()
        : this(new MetaQuant(Identifier.CreateAndRegister()))
    {
    }

    #endregion

    #region Branch operations

    public Roster<T> GetBranch(IIdentifier identifier)
    {
        return Branches.GetValueOrDefault(identifier) ?? throw new KeyNotFoundException();
    }

    public bool SaveBranch(Roster<T> branch)
    {
        return Branches.TryAdd(branch.UID, branch);
    }

    public Roster<T>? PopBranch(IIdentifier identifier)
    {
        Branches.TryRemove(identifier, out var branch);
        return branch;
    }

    public Roster<T> NewBranch(MetaQuant meta, Dictionary<IIdentifier, T> data)
    {
        var branch = new Roster<T>(meta, data);
        SaveBranch(branch);
        return branch;
    }

    public Roster<T> NewBranch(MetaQuant metaData) => NewBranch(metaData, new Dictionary<IIdentifier, T>());

    public bool ContainsBranch(IIdentifier identifier) => Branches.ContainsKey(identifier);

    #endregion
}